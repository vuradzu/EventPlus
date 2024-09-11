import React from "react";
import { View } from "react-native";
import { Iconify } from "../Iconify/Iconify";
import { IconifyProps } from "../Iconify/types/types";
import { Typography } from "../Typography/Typography";

type TabIconProps = {
  iconProps: IconifyProps;
  name: string;
  focused: boolean;
};

export const TabIcon = ({ iconProps: icon, name, focused }: TabIconProps) => {
  return (
    <View className="items-center justify-center">
      <Iconify {...icon} width={24} height={24} />
      <Typography
        fontWeight={focused ? "semibold" : "regular"}
        style={{ color: icon.color, marginTop: 2 }}
      >
        {name}
      </Typography>
    </View>
  );
};
