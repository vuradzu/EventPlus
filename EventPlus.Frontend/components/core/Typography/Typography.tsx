import { Text, TextProps } from "react-native";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";
import { TypographyVariants } from "./types/TypographyVariants";
import {
  FontStyle,
  FontWeight,
  TypographyVariantsObject,
} from "./types/VariantsObject";

type TypographyProps = {
  variant?: TypographyVariants;
} & Omit<FontStyle, "fontWeight"> & {
    fontWeight?: FontWeight;
  } & ClassNameProps<TextProps>;

export const Typography = (props: TypographyProps) => {
  const {
    children,
    className,
    style: customStyle,
    variant = "b1",
    fontSize = TypographyVariantsObject[variant].fontSize,
    fontWeight = TypographyVariantsObject[variant].fontWeight,
    ...rest
  } = props;

  const styles = {
    ...TypographyVariantsObject[variant],
    fontSize,
    fontWeight,
  };

  return (
    <Text
      {...rest}
      style={[styles, customStyle]}
      className={classNames(className, {}, ["color-text-primary"])}
    >
      {children}
    </Text>
  );
};
