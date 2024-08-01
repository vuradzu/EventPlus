import React, { useMemo, useRef } from "react";
import {
  ColorValue,
  ImageSourcePropType,
  KeyboardTypeOptions,
  TextInput,
  View,
} from "react-native";
import { classNames } from "~/utils/helpers/classNames";
import FullyRoundedInputTitle from "./components/FullyRoundedInputTitle";
import { InputError } from "./components/InputError";
import { InputIcon } from "./components/InputIcon";
import { InputSymbolsCounter } from "./components/InputSymbolsCounter";
import { InputVariant } from "./types/InputVariant";

type InputFullyRounded = {
  variant?: InputVariant.FullyRounded;
};

type InputHalfRounded = {
  variant?: InputVariant.HalfRounded;
  multiline?: boolean;
  numberOnLines?: number;
};

type InputProps = {
  placeholder: string;
  icon?: ImageSourcePropType;
  iconColor?: ColorValue;
  disabled?: boolean;
  styles?: string;
  value?: string | null;
  onValueChange: (value?: string) => void;
  error?: string;
  variant?: InputVariant;
  maxLength?: number;
  keyboardType?: KeyboardTypeOptions;
} & (InputFullyRounded | InputHalfRounded);

const Input = (props: InputProps) => {
  const {
    styles,
    placeholder,
    icon,
    iconColor,
    disabled = false,
    value,
    onValueChange,
    error,
    maxLength,
    keyboardType = 'default',
    ...rest
  } = props;

  const inputRef = useRef<TextInput>(null);

  const variant = useMemo(
    () => rest.variant ?? InputVariant.HalfRounded,
    [rest.variant]
  );

  return (
    <View className={classNames(styles, {}, ["flex", "flex-col"])}>
      <View
        className={classNames(
          "flex",
          {
            "rounded-full": variant === InputVariant.FullyRounded,
            "rounded-xl": variant === InputVariant.HalfRounded,
          },
          [
            "flex-row",
            "w-full",
            "bg-input-bg",
            "flex-row",
            "items-center",
            "justify-center",
          ]
        )}
        onTouchStart={() => inputRef.current?.focus()}
      >
        <InputIcon icon={icon} iconColor={iconColor} />
        <FullyRoundedInputTitle variant={variant} placeholder={placeholder} />
        <TextInput
          keyboardType={keyboardType}
          editable={!disabled}
          selectTextOnFocus={disabled}
          ref={inputRef}
          maxLength={maxLength}
          className={classNames(
            "flex-1",
            {
              "color-input-text-primary": !disabled,
              "color-input-text-secondary": disabled,
              "mx-4": variant === InputVariant.HalfRounded,
              "mr-4": variant === InputVariant.FullyRounded,
              "p-0":
                rest.variant === InputVariant.HalfRounded && !!rest.multiline,
            },
            ["text-[14px]", "my-5"]
          )}
          style={
            rest.variant === InputVariant.HalfRounded &&
            !!rest.numberOnLines &&
            rest.numberOnLines > 1
              ? {
                  height: rest.numberOnLines * 19,
                }
              : {}
          }
          value={value ?? undefined}
          onChangeText={(text) => {
            if (disabled) return;

            if (text === "") {
              onValueChange(undefined);
              return;
            }

            onValueChange(text);
          }}
          placeholder={variant === InputVariant.HalfRounded ? placeholder : ""}
          placeholderTextColor="#FFFFFF80"
          textAlignVertical="center"
          multiline={
            rest.variant === InputVariant.HalfRounded
              ? rest.multiline ?? false
              : false
          }
          numberOfLines={
            rest.variant === InputVariant.HalfRounded
              ? rest.numberOnLines ?? 1
              : 1
          }
        />
        <InputSymbolsCounter maxLength={maxLength} value={value} />
      </View>
      <InputError error={error} />
    </View>
  );
};

export default Input;
