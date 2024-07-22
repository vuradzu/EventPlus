import React from "react";
import { View } from "react-native";
import { IconButton } from "~/components/core/Button/IconButton";
import { Typography } from "~/components/core/Typography/Typography";

import FilterIcon from '~/assets/icons/filter.png';

export const HomeFilter = () => {
  return (
    <View className="flex flex-row justify-between items-center p-4">
      <View className="w-70">
        <Typography className="color-text-secondary">Filter hello</Typography>
      </View>
      <View className="w-30">
        <IconButton icon={FilterIcon} imageStyles="w-8"/>
      </View>
    </View>
  );
};
