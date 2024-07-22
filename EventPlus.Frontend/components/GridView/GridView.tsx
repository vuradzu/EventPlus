import React from "react";
import { View } from "react-native";

interface GridViewProps<T> {
  items: T[];
  renderItem(item: T): JSX.Element;
  columns?: number;
}

export const GridView = <T extends { id: any }>(props: GridViewProps<T>) => {
  const { items, renderItem, columns = 3 } = props;
  return (
    <View
      className="w-full h-full flex flex-row flex-wrap"
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
