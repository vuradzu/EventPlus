import { Tabs } from "expo-router";
import React from "react";
import { TabIcon } from "~/components/core/TabIcon/TabIcon";

export const TabBarHeight = 95;

const TabsLayout = () => {
  return (
    <>
      <Tabs
        screenOptions={{
          tabBarShowLabel: false,
          tabBarActiveTintColor: "#F9F9F9",
          tabBarInactiveTintColor: "#FFFFFF80",
          tabBarStyle: {
            backgroundColor: "#1E1E1E",
            borderTopWidth: 1,
            borderTopColor: "#FFFFFF14",
            height: TabBarHeight,
          },
        }}
      >
        <Tabs.Screen
          name="home/home"
          options={{
            headerShown: false,
            tabBarIcon: ({ color, focused }) => (
              <TabIcon
                iconProps={{
                  icon: "akar-icons:home",
                  color
                }}
                name="Дім"
                focused={focused}
              />
            ),
          }}
        />
        <Tabs.Screen
          name="tasks/tasks"
          options={{
            headerShown: false,
            tabBarIcon: ({ color, focused }) => (
              <TabIcon
                iconProps={{
                  icon: "akar-icons:check-box",
                  color
                }}
                name="Завдання"
                focused={focused}
              />
            ),
          }}
        />
        <Tabs.Screen
          name="profile"
          options={{
            headerShown: false,
            tabBarIcon: ({ color, focused }) => (
              <TabIcon
                iconProps={{
                  icon: "akar-icons:person",
                  color
                }}
                name="Профіль"
                focused={focused}
              />
            ),
          }}
        />
      </Tabs>
    </>
  );
};

export default TabsLayout;
