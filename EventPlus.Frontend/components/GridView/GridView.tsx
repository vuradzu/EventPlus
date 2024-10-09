import React, { useCallback, useState } from "react";
import { RefreshControl, ScrollView, View } from "react-native";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";

interface GridViewProps<T> {
  items?: T[];
  renderItem(item: T): JSX.Element;
  isLoading?: boolean;
  loadingRenderItem?(): JSX.Element;
  loadingItemsCount?: number;
  columns?: number;
  onRefresh?: () => Promise<void>;
}

export const GridView = <T extends { id: any }>(
  props: ClassNameProps<GridViewProps<T>, false>
) => {
  const {
    styles,
    items,
    renderItem,
    isLoading,
    loadingRenderItem,
    loadingItemsCount,
    columns = 3,
    onRefresh,
  } = props;

  const [refreshing, setRefreshing] = useState<boolean>(false);
  const onRefreshHandler = useCallback(() => {
    setRefreshing(true);
    if (onRefresh) onRefresh()?.then(() => setRefreshing(false));
  }, []);

  return (
    <ScrollView
      className={classNames(styles, {}, ["w-full", "h-full"])}
      contentContainerStyle={{
        width: "100%",
        display: "flex",
        flexDirection: "row",
        flexWrap: "wrap",
        rowGap: 38,
      }}
      refreshControl={
        !!onRefresh ? (
          <RefreshControl
            refreshing={refreshing}
            onRefresh={onRefreshHandler}
            tintColor="#D9D9D9"
          />
        ) : undefined
      }
    >
      {(isLoading ?? refreshing) && !!loadingRenderItem
        ? Array.from(Array(loadingItemsCount ?? 3).keys()).map((i) => (
            <View
              key={i}
              style={{ width: `${100 / columns}%` }}
              className="justify-center items-center"
            >
              {loadingRenderItem()}
            </View>
          ))
        : items?.map((item) => (
            <View
              key={item.id}
              style={{ width: `${100 / columns}%` }}
              className="justify-center items-center"
            >
              {renderItem(item)}
            </View>
          ))}
    </ScrollView>
  );
};
