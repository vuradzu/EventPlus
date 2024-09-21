import React from "react";
import {
  ActivityIndicator,
  TouchableOpacity,
  TouchableOpacityProps,
} from "react-native";
import { classNames } from "~/utils/helpers/classNames";
import { IconifyProps } from "../Iconify/types/types";
import { Typography } from "../Typography/Typography";
import { ButtonIcon } from "./components/ButtonIcon";

export enum ButtonVariants {
  Transparent,
  Primary,
  PrimaryBold,
  Secondary,
  SecondaryBold,
}

type ButtonProps = {
  variant?: ButtonVariants;
  iconProps?: IconifyProps;
  iconPosition?: "left" | "right";
  styles?: string;
  fontSize?: number;
  isLoading?: boolean;
  textStyles?: string;
} & Omit<TouchableOpacityProps, "className">;

export const Button = (props: ButtonProps) => {
  const {
    children,
    styles,
    iconProps,
    iconPosition,
    fontSize,
    variant = ButtonVariants.Primary,
    isLoading = false,
    textStyles,
    ...rest
  } = props;

  return (
    <TouchableOpacity
      {...rest}
      className={classNames(
        styles,
        {
          "bg-button-bg-primary":
            variant === ButtonVariants.Primary ||
            variant === ButtonVariants.PrimaryBold,
          "bg-button-bg-secondary":
            variant === ButtonVariants.Secondary ||
            variant === ButtonVariants.SecondaryBold,
          "flex-row rounded-full min-h-[54px] justify-center items-center py-[16px] w-full":
            variant !== ButtonVariants.Transparent,
        },
        []
      )}
    >
      {(!iconPosition || iconPosition === "left") && (
        <ButtonIcon iconProps={iconProps} />
      )}
      <Typography
        fontWeight={
          variant === ButtonVariants.PrimaryBold ||
          variant === ButtonVariants.SecondaryBold
            ? "bold"
            : "regular"
        }
        fontSize={fontSize}
        className={classNames(
          textStyles,
          { "mx-[8px]": variant !== ButtonVariants.Transparent },
          ["color-button-text", "items-center", "justify-center"]
        )}
      >
        {children}
      </Typography>
      {isLoading && <ActivityIndicator />}
      {iconPosition === "right" && <ButtonIcon iconProps={iconProps} />}
    </TouchableOpacity>
  );
};
