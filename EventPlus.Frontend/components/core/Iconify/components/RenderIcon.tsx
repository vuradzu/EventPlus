import { SvgXml } from "react-native-svg";
import { IconifyProps, IconProps } from "../types/types";
import { prepareSvgIcon } from "./services";

export const IconRenderer = (props: IconProps) => {
  const defaultProps: IconifyProps = {
    size: 24,
    color: "currentColor",
    ...props,
  };

  const svg = prepareSvgIcon(props.iconData, defaultProps);

  if (!props.iconData || !svg || !svg.body) {
    return null;
  }

  return (
    <SvgXml
      xml={svg.body}
      width={props.width ?? svg.attributes.width}
      height={props.height ?? svg.attributes.height}
      color={props.color}
    />
  );
};
