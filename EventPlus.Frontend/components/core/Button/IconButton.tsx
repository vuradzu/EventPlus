import React from "react";
import {
  ColorValue,
  ImageSourcePropType,
  TouchableOpacity,
  TouchableOpacityProps,
} from "react-native";
import { ButtonIcon } from "./components/ButtonIcon";

type IconButtonProps = {
  styles?: string;
  imageStyles?: string;
  icon?: ImageSourcePropType;
  iconColor?: ColorValue;
} & Omit<TouchableOpacityProps, "className">;

export const IconButton = (props: IconButtonProps) => {
  const { styles, imageStyles, icon, iconColor, ...rest } = props;

  return (
    <TouchableOpacity
      {...rest}
      className={styles}
    >
      <ButtonIcon icon={icon} iconColor={iconColor} styles={imageStyles} />
    </TouchableOpacity>
  );
};
