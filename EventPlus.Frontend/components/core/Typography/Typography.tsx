import { useMemo } from "react";
import { Text, TextProps } from "react-native";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";
import { getFontByWeight } from "./services/typographyHelpers";
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
  };

  const fontClass = useMemo(() => getFontByWeight(fontWeight), [fontWeight]);

  return (
    <Text
      {...rest}
      className={classNames(className, {}, ["color-text-primary", fontClass])}
      style={[styles, customStyle]}
    >
      {children}
    </Text>
  );
};
