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
        },
        [
          "flex-row",
          "rounded-full",
          "min-h-[54px]",
          "justify-center",
          "items-center",
          "py-[16px]",
          "w-full",
        ]
      )}
    >
      {(!iconPosition || iconPosition === "left") && (
        <ButtonIcon iconProps={iconProps} />
      )}
      <Typography
        fontWeight={
          variant === ButtonVariants.PrimaryBold ||
          variant === ButtonVariants.SecondaryBold
            ? "semibold"
            : "regular"
        }
        fontSize={fontSize}
        className="color-button-text items-center justify-center mx-[8px]"
      >
        {children}
      </Typography>
      {isLoading && <ActivityIndicator />}
      {iconPosition === "right" && <ButtonIcon iconProps={iconProps} />}
    </TouchableOpacity>
  );
};
