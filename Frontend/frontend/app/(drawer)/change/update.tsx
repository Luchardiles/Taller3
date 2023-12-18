import { Drawer } from "expo-router/drawer";
import { DrawerToggleButton } from "@react-navigation/drawer";
import UpdateScreen from "../../../components/change/UpdateScreen";

const Edit = () => {
  return (
    <>
      <Drawer.Screen
        options={{
          headerShown: true,
          title: "Cambiar contraseña",
          headerLeft: () => <DrawerToggleButton />,
        }}
      />
      <UpdateScreen />
    </>
  );
};

export default Edit;
