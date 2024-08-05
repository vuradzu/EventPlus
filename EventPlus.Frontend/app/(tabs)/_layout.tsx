import { Tabs } from "expo-router";
import React from "react";
import { TabIcon } from "~/components/core/TabIcon/TabIcon";

import HomeIcon from "~/assets/icons/tabs/home.png";
import ProfileIcon from "~/assets/icons/tabs/profile.png";

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
                icon={HomeIcon}
                color={color}
                name="Дім"
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
                icon={ProfileIcon}
                color={color}
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
