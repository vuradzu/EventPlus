import { Iconify } from "../../Iconify/Iconify";
import { IconifyProps } from "../../Iconify/types/types";

type ButtonIconProps = {
  iconProps?: IconifyProps;
};

export const ButtonIcon = ({ iconProps }: ButtonIconProps) => {
  if (!iconProps?.icon) return null;

  return <Iconify {...iconProps} />;
};
