import React from "react";
import { View } from "react-native";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";

interface GridViewProps<T> {
  items: T[];
  renderItem(item: T): JSX.Element;
  columns?: number;
}

export const GridView = <T extends { id: any }>(
  props: ClassNameProps<GridViewProps<T>, false>
) => {
  const { styles, items, renderItem, columns = 3 } = props;
  return (
    <View
      className={classNames(styles, {}, [
        "w-full",
        "h-full",
        "flex",
        "flex-row",
        "flex-wrap",
      ])}
      style={{ rowGap: 38 }}
    >
      {items.map((item) => (
        <View
          key={item.id}
          style={{ width: `${100 / columns}%` }}
          className="justify-center items-center"
        >
          {renderItem(item)}
        </View>
      ))}
    </View>
  );
};
