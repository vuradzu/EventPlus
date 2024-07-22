import React from "react";
import { ColorValue, Image, ImageSourcePropType } from "react-native";

interface InputIconProps {
  icon?: ImageSourcePropType;
  iconColor?: ColorValue;
}

export const InputIcon = ({ icon, iconColor }: InputIconProps) => {
  return !!icon ? (
    <Image source={icon} resizeMode="contain" tintColor={iconColor} />
  ) : null;
};
