import React from "react";
import { Image, ImageSourcePropType, View } from "react-native";
import { Typography } from "../Typography/Typography";
import { TypographyVariants } from "../Typography/types/TypographyVariants";

type TabIconProps = {
  icon: ImageSourcePropType;
  color: string;
  name: string;
  focused: boolean;
};

export const TabIcon = ({ icon, color, name, focused }: TabIconProps) => {
  return (
    <View className="items-center justify-center gap-1">
      <Image
        source={icon}
        resizeMode="contain"
        tintColor={color}
        className="w-6 h-6"
      />
      <Typography
        variant={
          focused ? TypographyVariants.Semibold : TypographyVariants.Regular
        }
        style={{ color }}
      >
        {name}
      </Typography>
    </View>
  );
};
