import { Text, TextProps } from "react-native";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";
import { TypographyVariants } from "./types/TypographyVariants";
import { FontStyle, TypographyVariantsObject } from "./types/VariantsObject";

type TypographyProps = {
  variant?: TypographyVariants;
} & FontStyle &
  ClassNameProps<TextProps>;

export const Typography = (props: TypographyProps) => {
  const {
    children,
    className,
    style: customStyle,
    variant = TypographyVariants.Regular,
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
      className={classNames(
        className,
        {
          "font-sfregular": variant === TypographyVariants.Regular,
          "font-sfsemibold": variant === TypographyVariants.Semibold,
        },
        ["color-text-primary"]
      )}
      style={[styles, customStyle]}
    >
      {children}
    </Text>
  );
};
