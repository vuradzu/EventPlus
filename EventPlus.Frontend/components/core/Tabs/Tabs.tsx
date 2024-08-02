import React, { useCallback, useState } from "react";
import { LayoutChangeEvent, Pressable, View } from "react-native";
import Animated, {
  runOnJS,
  useAnimatedStyle,
  useSharedValue,
  withTiming,
} from "react-native-reanimated";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";
import { Typography } from "../Typography/Typography";

export type Tab = {
  id: string;
  title: string;
};

interface TabsProps {
  tabs: Tab[];
  selectedTab?: string;
  onTabChange?: (tabId: string) => void;
}

export const Tabs = (props: ClassNameProps<TabsProps, false>) => {
  const { styles, tabs, selectedTab, onTabChange } = props;
  const [selectedTabDimensions, setSelectedTabDimenstions] = useState({
    height: 20,
    width: 100,
  });

  const tabWidth = selectedTabDimensions.width / tabs.length;
  const getTabIndex = useCallback(
    (tabId?: string) =>
      tabs.map((t, i) => ({ t, i })).find((o) => o.t.id === tabId)?.i ?? 0,
    [tabs]
  );

  const onTabbarLayout = (e: LayoutChangeEvent) =>
    setSelectedTabDimenstions({
      height: e.nativeEvent.layout.height,
      width: e.nativeEvent.layout.width,
    });

  //4 is a magic number :)
  const tabPositionX = useSharedValue(getTabIndex(selectedTab) * tabWidth * 4);

  const handlePress = (tabIndex: number) => {
    const tabId = tabs.find((_, i) => i === tabIndex)!.id;
    !!onTabChange && onTabChange(tabId);
  };

  const onTabPress = (tabId: string) => {
    const tabIndex = getTabIndex(tabId);
    tabPositionX.value = withTiming(tabWidth * tabIndex, {}, () => {
      runOnJS(handlePress)(tabIndex);
    });
  };

  const animatedStyle = useAnimatedStyle(() => {
    return {
      transform: [{ translateX: tabPositionX.value }],
    };
  });

  return (
    <View
      className={classNames(styles, {}, [
        "w-full",
        "bg-input-bg",
        "rounded-xl",
        "justify-center",
      ])}
      accessibilityRole="tabbar"
    >
      <Animated.View
        className={`absolute bg-button-bg-primary rounded-xl mx-1`}
        style={[
          animatedStyle,
          {
            height: selectedTabDimensions.height - 10,
            width: tabWidth - 10,
          },
        ]}
      />
      <View onLayout={onTabbarLayout} className="flex-row">
        {tabs.map((tab) => (
          <Pressable
            key={tab.id}
            className="flex-1 py-5"
            onPress={() => onTabPress(tab.id)}
          >
            <Typography
              fontSize={14}
              className="color-button-primary self-center"
            >
              {tab.title}
            </Typography>
          </Pressable>
        ))}
      </View>
    </View>
  );
};
