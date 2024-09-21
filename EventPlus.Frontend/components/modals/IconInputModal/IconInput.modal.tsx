import React, { useState } from "react";
import { View } from "react-native";
import { useMagicModal } from "react-native-magic-modal";
import { IconButton } from "~/components/core/Button/IconButton";
import { GridView } from "~/components/GridView/GridView";
import { classNames } from "~/utils/helpers/classNames";

interface IconInputModalProps {
  selectedIconName?: string;
  icons?: string[];
  onIconChange: (iconName: string) => void;
}

export const IconInputModal = ({
  selectedIconName,
  icons,
  onIconChange,
}: IconInputModalProps) => {
  const { hide } = useMagicModal();

  const [selectedIcon, setSelectedIcon] = useState<string>(
    selectedIconName ?? "octicon:sparkle-fill-16"
  );

  const iconsToRender = icons ?? [
    "octicon:sparkle-fill-16", "fluent:tab-desktop-multiple-sparkle-20-filled", "fluent:mic-sparkle-20-filled", "fluent:camera-sparkles-24-filled", "fluent:hexagon-sparkle-24-filled",
    "fluent:arrow-trending-sparkle-24-filled", "fluent:flash-sparkle-20-filled", "fluent:rectangle-landscape-sparkle-32-filled", "fluent:search-sparkle-28-filled", "fluent:paint-brush-sparkle-20-filled",
    "fluent:text-effects-sparkle-24-filled", "fluent:hat-graduation-sparkle-16-filled", "fluent:pen-sparkle-16-filled", "fluent:bot-sparkle-24-filled", "fa6-solid:hand-sparkles",
];

  const onIconChangeHandler = (iconName: string) => {
    setSelectedIcon(iconName);
    onIconChange(iconName);
    hide();
  };

  return (
    <View className="flex flex-row justify-center items-center">
      <GridView
        styles={classNames("bg-bg-primary", {}, ["w-[95%]", "flex-initial", "rounded-xl", "py-5 px-3"])}
        columns={5}
        items={iconsToRender.map((icon, i) => ({ id: i, icon }))}
        renderItem={(item) => (
          <IconButton
            onPress={() => onIconChangeHandler(item.icon)}
            iconProps={{
              icon: item.icon,
              width: 60,
              height: 60,
              color: selectedIcon === item.icon ? "#7560F1" : "white",
            }}
          />
        )}
      ></GridView>
    </View>
  );
};
