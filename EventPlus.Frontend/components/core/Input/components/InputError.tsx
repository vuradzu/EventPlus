import React, { useEffect } from "react";
import Animated, {
  useAnimatedStyle,
  useSharedValue,
  withTiming,
} from "react-native-reanimated";
import { Typography } from "../../Typography/Typography";

interface InputErrorProps {
  error?: string;
}

export const InputError = ({ error }: InputErrorProps) => {
  const errorHeightAnimated = useSharedValue(0);
  const errorOpacityAnimated = useSharedValue(0);
  const errorAnimatedStyle = useAnimatedStyle(() => {
    return {
      height: errorHeightAnimated.value,
      opacity: errorOpacityAnimated.value,
    };
  });

  useEffect(() => {
    errorHeightAnimated.value = withTiming(!!error ? 25 : 0);
    errorOpacityAnimated.value = withTiming(!!error ? 1 : 0, { duration: 400 });
  }, [error]);

  return (
    <Animated.View style={errorAnimatedStyle} className="flex justify-center">
      <Typography fontSize={14} className="color-error mx-4">
        {error}
      </Typography>
    </Animated.View>
  );
};
