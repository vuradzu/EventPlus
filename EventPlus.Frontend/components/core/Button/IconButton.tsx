import React from "react";
import {
  ColorValue,
  Image,
  ImageSourcePropType,
  TouchableOpacity,
  TouchableOpacityProps,
} from "react-native";
import { classNames } from "~/utils/helpers/classNames";
import { IconifyProps } from "../Iconify/types/types";
import { ButtonIcon } from "./components/ButtonIcon";

type IconProps =
  | IconifyProps
  | {
      icon?: ImageSourcePropType;
      color?: ColorValue;
      imageStyles?: string;
      size?: number;
    };

type IconButtonProps = {
  styles?: string;
  iconProps?: IconProps;
} & Omit<TouchableOpacityProps, "className">;

export const IconButton = (props: IconButtonProps) => {
  const { styles, iconProps, ...rest } = props;

  const isIconifyProps = (prop: IconProps): prop is IconifyProps =>
    typeof prop.icon === "string";

  if (!iconProps?.icon)
    return <TouchableOpacity {...rest} className={styles} />;

  return (
    <TouchableOpacity {...rest} className={styles}>
      {isIconifyProps(iconProps) ? (
        <ButtonIcon iconProps={iconProps} />
      ) : (
        <Image
          source={iconProps.icon}
          width={iconProps.size}
          height={iconProps.size}
          resizeMode="cover"
          tintColor={iconProps.color}
          className={classNames(styles, {}, ["h-6", "w-6"])}
        />
      )}
    </TouchableOpacity>
  );
};
