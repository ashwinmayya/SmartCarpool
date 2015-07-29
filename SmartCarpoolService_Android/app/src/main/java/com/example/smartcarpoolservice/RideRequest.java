package com.example.smartcarpoolservice;

import java.util.Date;

/**
 * Created by bisen on 7/27/2015.
 */
public abstract class RideRequest
{
    @com.google.gson.annotations.SerializedName("startlocation")
    public Location mStartLocation;

    @com.google.gson.annotations.SerializedName("endlocation")
    public Location mEndLocation;

    @com.google.gson.annotations.SerializedName("starttime")
    public Date mStartTime;

    @com.google.gson.annotations.SerializedName("points")
    public int mPoints;

    @com.google.gson.annotations.SerializedName("riderequeststatus")
    public RideRequestStatus mRideRequestStatus;

    @com.google.gson.annotations.SerializedName("user")
    public User mUser;
}