using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine
{
    public class ConfigSettings
    {
        public static void InitConfigSettings(string assemblyMainDirectory)
        {
            assembly_main_dir = assemblyMainDirectory + @"\";
            prop_dir_root = assemblyMainDirectory + @"\Assets\Properties\";

            Dictionary<string, string> configProps = PropertyUtil.LoadPropertiesFromFile(prop_dir_root + @"master.prop");
            starting_screen_width = int.Parse(configProps["starting_screen_width"]);
            starting_screen_height = int.Parse(configProps["starting_screen_height"]);

            texture_directory = configProps["texture_directory"];

            camera_buffer = int.Parse(configProps["camera_buffer"]);
            camera_type = configProps["camera_type"];
            int x = configProps.ContainsKey("camera_scroll_direction_x") ? int.Parse(configProps["camera_scroll_direction_x"]) : 0;
            int y = configProps.ContainsKey("camera_scroll_direction_y") ? int.Parse(configProps["camera_scroll_direction_y"]) : 0;
            camera_scroll_direction = new Vector(x, y);

            clock_speed = double.Parse(configProps["clock_speed"]);

            player_speed = double.Parse(configProps["player_speed"]);
            default_non_player_speed = double.Parse(configProps["default_non_player_speed"]);
            gravity_pps = double.Parse(configProps["gravity_pps"]);
            max_x_velocity = double.Parse(configProps["max_x_velocity"]);
            max_y_velocity = double.Parse(configProps["max_y_velocity"]);

            log_directory = configProps["log_directory"];
        }

        // File paths
        private static string assembly_main_dir;
        public static string ASSEMBLY_MAIN_DIR { get { return assembly_main_dir; } }

        private static string prop_dir_root;
        public static string PROP_DIR_ROOT { get { return prop_dir_root; } }

        // Screen Size
        private static int starting_screen_width;
        public static int STARTING_SCREEN_WIDTH { get { return starting_screen_width; } }

        private static int starting_screen_height;
        public static int STARTING_SCREEN_HEIGHT { get { return starting_screen_height; } }

        // Textures
        private static string texture_directory;
        public static string TEXTURE_DIRECTORY { get { return @"Assets\Textures\"; } }

        // Camera
        private static int camera_buffer;
        public static int CAMERA_BUFFER { get { return camera_buffer; } }

        private static string camera_type;
        public static String CAMERA_TYPE { get { return camera_type; } }

        private static Vector camera_scroll_direction;
        public static Vector CAMERA_SCROLL_DIRECTION { get { return camera_scroll_direction; } }

        // Clock
        private static double clock_speed;
        public static double CLOCK_SPEED { get { return clock_speed; } }

        // Physics Parameters
        private static double player_speed;
        public static double PLAYER_SPEED { get { return player_speed; } }

        private static double default_non_player_speed;
        public static double DEFAULT_NON_PLAYER_SPEED { get { return default_non_player_speed; } }

        private static double gravity_pps;
        public static double GRAVITY_PPS { get { return gravity_pps; } }

        private static double max_x_velocity;
        public static double MAX_X_VELOCITY { get { return max_x_velocity; } }

        private static double max_y_velocity;
        public static double MAX_Y_VELOCITY { get { return max_y_velocity; } }

        // Logger
        private static string log_directory;
        public static string LOG_DIRECTORY { get { return log_directory; } }

    }
}
