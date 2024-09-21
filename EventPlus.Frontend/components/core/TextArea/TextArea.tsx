import React, { useMemo, useRef } from "react";
import {
  ColorValue,
  Image,
  ImageSourcePropType,
  TextInput,
  TextInputProps,
  View,
} from "react-native";
import { classNames } from "~/utils/helpers/classNames";
import { Typography } from "../Typography/Typography";

type TextAreaProps = {
  title: string;
  placeholder?: string;
  icon?: ImageSourcePropType;
  iconColor?: ColorValue;
  disabled?: boolean;
  styles?: string;
  value?: string | null;
  onValueChange: (value?: string) => void;
  error?: boolean | string;
} & TextInputProps;

export const TextArea = (props: TextAreaProps) => {
  const {
    title,
    numberOfLines = 3,
    disabled = false,
    styles,
    placeholder,
    icon,
    iconColor,
    value,
    onValueChange,
    error,
  } = props;

  const inputRef = useRef<TextInput>(null);

  const textAreaHeight = useMemo(
    () => 22 * (numberOfLines - 1) + 54,
    [numberOfLines]
  );

  return (
    <View
      className={classNames(styles, {}, [
        "rounded-xl",
        "flex-row",
        "space-y-2",
        "w-full",
        `h-[${textAreaHeight}px]`,
        "bg-input-bg",
        "flex-row",
        "px-5",
        "pt-4",
      ])}
      onTouchStart={() => inputRef.current?.focus()}
    >
      {!!icon && (
        <Image source={icon} resizeMode="contain" tintColor={iconColor} />
      )}
      <Typography
        variant="b2"
        className={classNames(
          "mr-5",
          {
            "color-input-text-secondary": !error,
            "color-red-800": !!error,
          },
          []
        )}
      >
        {title}
      </Typography>
      <TextInput
        editable={!disabled}
        selectTextOnFocus={disabled}
        ref={inputRef}
        multiline
        numberOfLines={numberOfLines}
        className={classNames(
          "flex-1",
          {
            "color-input-text-primary": !disabled,
            "color-input-text-secondary": disabled,
          },
          ["mr-5", "text-[14px]", "mb-2"]
        )}
        value={value ?? undefined}
        onChangeText={(text) => {
          if (disabled) return;

          if (text === "") {
            onValueChange(undefined);
            return;
          }

          onValueChange(text.substring(0, length));
        }}
        placeholder={placeholder}
      />
    </View>
  );
};
