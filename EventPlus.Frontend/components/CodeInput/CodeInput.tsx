import React, { useState } from "react";
import { StyleSheet, View } from "react-native";
import {
  CodeField,
  Cursor,
  useBlurOnFulfill,
  useClearByFocusCell,
} from "react-native-confirmation-code-field";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";
import { InputError } from "../core/Input/components/InputError";
import { Typography } from "../core/Typography/Typography";

interface CodeInputProps {
  cellsCount: number;
  value: string;
  setValue: (value: string) => void;
  error?: string;
}

const CodeInput = (props: ClassNameProps<CodeInputProps, false>) => {
  const { styles, cellsCount, value, setValue, error } = props;

  const ref = useBlurOnFulfill({ value, cellCount: cellsCount });
  const [fieldProps, getCellOnLayoutHandler] = useClearByFocusCell({
    value,
    setValue,
  });

  return (
    <View className={classNames(styles, {}, [])}>
      <CodeField
        ref={ref}
        {...fieldProps}
        // Use `caretHidden={false}` when users can't paste a text value, because context menu doesn't appear
        value={value}
        onChangeText={setValue}
        cellCount={cellsCount}
        keyboardType="ascii-capable"
        textContentType="oneTimeCode"
        autoComplete="one-time-code"
        renderCell={({ index, symbol, isFocused }) => (
          <Typography
            key={index}
            style={[
              {
                ...cellStyles.cell,
                borderColor: !!error ? "#DC3545" : "#FFFFFF80",
              },
              isFocused && cellStyles.focusCell,
            ]}
            onLayout={getCellOnLayoutHandler(index)}
          >
            {symbol || (isFocused ? <Cursor /> : null)}
          </Typography>
        )}
      />
      <InputError error={error} styles="m-0" />
    </View>
  );
};

const cellStyles = StyleSheet.create({
  root: { flex: 1, padding: 20 },
  title: { textAlign: "center", fontSize: 30 },
  cell: {
    width: 52,
    height: 48,
    fontSize: 24,
    borderWidth: 1.5,
    lineHeight: 42,
    borderColor: "#FFFFFF80",
    borderRadius: 8,
    textAlign: "center",
    color: "#FFFFFF",
  },
  focusCell: {
    borderColor: "#FFFFFF",
  },
});

export default CodeInput;
