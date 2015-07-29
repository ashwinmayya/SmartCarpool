package com.example.smartcarpoolservice;

import android.app.IntentService;
import android.content.Intent;
import android.location.Address;
import android.location.Geocoder;
import android.location.Location;
import android.os.Bundle;
import android.os.Handler;
import android.os.ResultReceiver;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

/**
 * Created by ramyasen on 7/28/2015.
 */
public class FetchAddressIntentService extends IntentService {


    protected ResultReceiver mReceiver;

    public FetchAddressIntentService(String name, ResultReceiver mReceiver) {
        super(name);
        this.mReceiver = mReceiver;
    }

    public FetchAddressIntentService() {
        super("FetchAdressIntentService");
    }

    private void deliverResultToReceiver(int resultCode, String message) {
        Bundle bundle = new Bundle();
        bundle.putString(Constants.RESULT_DATA_KEY, message);
        mReceiver.send(resultCode, bundle);
    }

    @Override
    protected void onHandleIntent(Intent intent) {
        Geocoder geocoder = new Geocoder(this, Locale.getDefault());
        // Get the location passed to this service through an extra.
        Location location = intent.getParcelableExtra(
                Constants.LOCATION_DATA_EXTRA);

        List<Address> addresses = null;

        try {
            addresses = geocoder.getFromLocation(location.getLatitude(),location.getLongitude(),
                    // In this sample, get just a single address.
                    1);
        } catch (IOException ioException) {
            // Catch network or other I/O problems.
           // errorMessage = getString(R.string.service_not_available);
            Log.e("1", "Service not available", ioException);
        } catch (IllegalArgumentException illegalArgumentException) {
            // Catch invalid latitude or longitude values.
            //errorMessage = getString(R.string.invalid_lat_long_used);
            Log.e("2", "Invalid lat long" + ". " +
                    "Latitude = " + location.getLatitude() +
                    ", Longitude = " +
                    location.getLongitude(), illegalArgumentException);
        }

        // Handle case where no address was found.
        if (addresses == null || addresses.size()  == 0) {
           // if (errorMessage.isEmpty()) {
                //errorMessage = getString(R.string.no_address_found);
                Log.e("3","No address found");
           // }
            deliverResultToReceiver(Constants.FAILURE_RESULT, "");
        } else {
            Address address = addresses.get(0);
            ArrayList<String> addressFragments = new ArrayList<String>();

            // Fetch the address lines using getAddressLine,
            // join them, and send them to the thread.
            for(int i = 0; i < address.getMaxAddressLineIndex(); i++) {
                addressFragments.add(address.getAddressLine(i));
            }
            Log.i("5","address found");
            deliverResultToReceiver(Constants.SUCCESS_RESULT,
                    TextUtils.join(System.getProperty("line.separator"),
                            addressFragments));
        }
    }
}

class AddressResultReceiver extends ResultReceiver {
    public AddressResultReceiver(Handler handler) {
        super(handler);
    }

    @Override
    protected void onReceiveResult(int resultCode, Bundle resultData) {

        // Display the address string
        // or an error message sent from the intent service.
        String addressOutput = resultData.getString(Constants.RESULT_DATA_KEY);
        displayAddressOutput(addressOutput);

        // Show a toast message if an address was found.
        if (resultCode == Constants.SUCCESS_RESULT) {
            Toast.makeText(null, "msg msg", Toast.LENGTH_SHORT).show();
        }

    }

    private void displayAddressOutput(String addressOutput)
    {
        SmartCarpoolHomepage.editText.setText(addressOutput);
    }
}

