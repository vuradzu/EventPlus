import type { IconifyIconBuildResult } from "@iconify/utils";
import type { XmlProps } from "react-native-svg";
import { IconData } from "./iconData";

export interface IconifyProps extends Omit<XmlProps, "xml"> {
  icon: string;
  size?: number;
}

export interface NativeIconProps extends IconifyProps {
  svg: IconifyIconBuildResult;
}

export interface IconProps extends IconifyProps {
  iconData: IconData;
}
