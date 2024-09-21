import React from "react";
import { View } from "react-native";
import { Typography } from "~/components/core/Typography/Typography";

import { Iconify } from "~/components/core/Iconify/Iconify";

export const EventsFilter = () => {
  return (
    <View className="flex flex-row justify-between items-center p-4">
      <View className="w-70">
        <Typography variant="h5" fontWeight="bold" className="color-text-secondary">Filter</Typography>
      </View>
      <View className="w-30">
        <Iconify icon="mdi:mixer-settings" color="white"/>
      </View>
    </View>
  );
};
