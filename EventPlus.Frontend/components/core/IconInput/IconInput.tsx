import React, { useCallback, useState } from "react";
import { View } from "react-native";
import { magicModal } from "react-native-magic-modal";
import { IconInputModal } from "~/components/modals/IconInputModal/IconInput.modal";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";
import { IconButton } from "../Button/IconButton";

export interface IconInputProps {
  icon?: string;
  onIconChange: (iconName: string) => void;
}

export const IconInput = ({
  styles,
  icon,
  onIconChange,
}: ClassNameProps<IconInputProps, false>) => {
  const [selectedIcon, setSelectedIcon] = useState<string>(
    icon ?? "octicon:sparkle-fill-16"
  );

  const iconChangeHandler = (iconName: string) => {
    setSelectedIcon(iconName);
    onIconChange(iconName);
  };

  const showIconInputModal = useCallback(() => {
    magicModal.show(
      () => (
        <IconInputModal
          selectedIconName={selectedIcon}
          onIconChange={iconChangeHandler}
        />
      ),
      {
        swipeDirection: undefined,
      }
    );
  }, [selectedIcon]);

  return (
    <View
      className={classNames(styles, {}, [
        "rounded-xl",
        "bg-input-bg",
        "p-2",
        "flex",
        "flex-row",
        "justify-center",
        "items-center",
      ])}
    >
      <IconButton
        onPress={showIconInputModal}
        iconProps={{
          icon: selectedIcon,
          width: 40,
          height: 40,
          color: "white",
        }}
      />
    </View>
  );
};
