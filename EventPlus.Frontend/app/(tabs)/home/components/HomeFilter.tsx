import React from "react";
import { View } from "react-native";
import { Typography } from "~/components/core/Typography/Typography";

import { Iconify } from "~/components/core/Iconify/Iconify";

export const HomeFilter = () => {
  return (
    <View className="flex flex-row justify-between items-center p-4">
      <View className="w-70">
        <Typography variant="h5" className="color-text-secondary">Filter hello</Typography>
      </View>
      <View className="w-30">
        <Iconify icon="mdi:mixer-settings" color="white"/>
        {/* <IconButton icon={} imageStyles="w-8"/> */}
      </View>
    </View>
  );
};
