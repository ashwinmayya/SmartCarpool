package com.example.smartcarpoolservice;

import android.content.Context;
import android.view.View;
import android.widget.EditText;

import com.microsoft.windowsazure.mobileservices.MobileServiceClient;
import com.microsoft.windowsazure.mobileservices.table.MobileServiceTable;

import java.net.MalformedURLException;
import java.util.concurrent.ExecutionException;

/**
 * Created by ashwinm on 7/29/2015.
 */
public class MobileServiceHelper {

    MobileServiceClient mClient;

    public MobileServiceHelper(Context context){
        try {
            mClient = new MobileServiceClient(
                    "https://smartcarpoolservice.azure-mobile.net/",
                    "LVxoMLwxEaIYGtRCmtbgLYGPwLuIMJ27", context);
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
    }

    public User AddNewUser(User user) throws ExecutionException, InterruptedException
    {
        if(mClient == null)
        {
            return null;
        }

        MobileServiceTable<User> mUserTable = mClient.getTable(User.class);
        return mUserTable.insert(user).get();
    }

    public void AddNewPassengerRequest(){
        if(mClient == null)
        {
            return;
        }

        MobileServiceTable<User> mUserTable = mClient.getTable(User.class);
//        User newUser = mUserTable.insert(user).get();
    }

}


