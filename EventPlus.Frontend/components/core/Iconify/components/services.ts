import {
  type IconifyIconBuildResult,
  iconToHTML,
  iconToSVG,
} from "@iconify/utils";
import { IconData } from "../types/iconData";
import { IconifyProps } from "../types/types";

export const prepareSvgIcon = (
  iconData: IconData,
  props: IconifyProps
): IconifyIconBuildResult => {
  const iconBuildResult = iconToSVG(iconData, {
    height: props.size,
  });

  return {
    ...iconBuildResult,
    body: iconToHTML(iconBuildResult.body, iconBuildResult.attributes),
  };
};
