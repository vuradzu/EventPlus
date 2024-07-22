import { ColorValue, Image, ImageSourcePropType } from "react-native";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";

type ButtonIconProps = {
  styles?: string;
  size?: number;
  icon?: ImageSourcePropType;
  iconColor?: ColorValue;
};

export const ButtonIcon = (props: ButtonIconProps) => {
  const { styles, size, icon, iconColor } = props;
  if (!icon) return null;

  return (
    <Image
      source={icon}
      width={size}
      height={size}
      resizeMode="cover"
      tintColor={iconColor}
      className={classNames(styles, {}, ["h-6", "w-6"])}
    />
  );
};
