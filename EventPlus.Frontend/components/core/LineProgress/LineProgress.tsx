import React, { useMemo } from "react";
import { StyleProp, View, ViewStyle } from "react-native";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";

interface LineProgressProps {
  unfilledStyles?: string;
  progress: number;
  height?: number;
}

export const LineProgress = (
  props: ClassNameProps<LineProgressProps, false>
) => {
  const { styles, unfilledStyles, progress, height = 6 } = props;

  const progressPercentage = useMemo(() => {
    const percentage = progress * 100;

    return percentage > 100 ? 100 : percentage;
  }, [progress]);

  const wrapperStyles: StyleProp<ViewStyle> = {
    borderRadius: 4,
    height,
  };

  const progressStyle: StyleProp<ViewStyle> = {
    width: `${progressPercentage}%`,
    borderRadius: 4,
    height,
  };

  return (
    <View
      style={wrapperStyles}
      className={classNames(unfilledStyles, {}, [
        "w-full, bg-[#D9D9D9]",
        "relative",
      ])}
    >
      <View
        style={progressStyle}
        className={classNames(styles, {}, [
          "bg-button-bg-primary",
          "absolute",
          "left-0",
          "top-0",
        ])}
      />
    </View>
  );
};
