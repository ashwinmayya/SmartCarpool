package com.example.smartcarpoolservice;

/**
 * Created by bisen on 7/27/2015.
 */
public class User {

    @com.google.gson.annotations.SerializedName("name")
    public String mName;

    @com.google.gson.annotations.SerializedName("authid")
    public String mAuthId;

    @com.google.gson.annotations.SerializedName("totalpoints")
    public int mTotalPoints;

    @com.google.gson.annotations.SerializedName("id")
    public String mId;
}
