package com.example.smartcarpoolservice;

/**
 * Created by ashwinm on 7/29/2015.
 */
public class Location {
    @com.google.gson.annotations.SerializedName("name")
    public String mName;

    @com.google.gson.annotations.SerializedName("lat")
    public double mLat;

    @com.google.gson.annotations.SerializedName("long")
    public double mLong;
}
