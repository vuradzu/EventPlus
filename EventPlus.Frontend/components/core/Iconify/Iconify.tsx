import { IconRenderer } from "./components/RenderIcon";
import IconNotFoundError from "./errors/IconNotFoundError";
import { IconData } from "./types/iconData";
import { IconifyProps } from "./types/types";

type TempIconifyProps = IconifyProps & {
  iconData?: IconData;
};

const icons = global.__ICONIFY__ICONS__;

/* @@iconify-code-gen */
export const Iconify = (props: IconifyProps) => {
  const tempProps = props as TempIconifyProps;

  const iconData = tempProps.iconData ?? icons?.[props.icon];

  if (!iconData) {
    if (!__DEV__) return null;

    throw IconNotFoundError(JSON.stringify(props.icon));
  }

  return <IconRenderer iconData={iconData} {...props} />;
};
