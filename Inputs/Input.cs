using FlowerSlayer.Objects;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;



namespace FlowerSlayer.Inputs
{
	class Input
	{
		private static List<Keys> keysDown = new();
		private static List<Keys> keysDownLast = new();
		private static List<MouseButton> buttonsDown = new();
		private static List<MouseButton> buttonsDownLast = new();

		public static void Initialize(Game game)
		{
			keysDown = new List<Keys>();
			keysDownLast = new List<Keys>();
			buttonsDown = new List<MouseButton>();
			buttonsDownLast = new List<MouseButton>();

			game.MouseDown += game_MouseDown;
			game.MouseUp += game_MouseUp;
			game.KeyDown += game_KeyDown;
			game.KeyUp += game_KeyUp;
		}

		static void game_KeyDown(KeyboardKeyEventArgs e)
		{
			if (!keysDown.Contains(e.Key))
				keysDown.Add(e.Key);
		}
		static void game_KeyUp(KeyboardKeyEventArgs e)
		{
			while (keysDown.Contains(e.Key))
				keysDown.Remove(e.Key);
		}

		static void game_MouseDown(MouseButtonEventArgs e)
		{
			if (!buttonsDown.Contains(e.Button))
				buttonsDown.Add(e.Button);
		}
		static void game_MouseUp(MouseButtonEventArgs e)
		{
			while (buttonsDown.Contains(e.Button))
				buttonsDown.Remove(e.Button);
		}

		public static void Update()
		{
			keysDownLast = new List<Keys>(keysDown);
			buttonsDownLast = new List<MouseButton>(buttonsDown);
		}

		public static bool KeyPress(Keys key)
		{
			return keysDown.Contains(key) && !keysDownLast.Contains(key);
		}
		public static bool KeyRelease(Keys key)
		{
			return !keysDown.Contains(key) && keysDownLast.Contains(key);
		}
		public static bool KeyDown(Keys key)
		{
			return (keysDown.Contains(key));
		}

		public static bool MousePress(MouseButton button)
		{
			return (buttonsDown.Contains(button) && !buttonsDownLast.Contains(button));
		}
		public static bool MouseRelease(MouseButton button)
		{
			return (!buttonsDown.Contains(button) && buttonsDownLast.Contains(button));
		}
		public static bool MouseDown(MouseButton button)
		{
			return (buttonsDown.Contains(button));
		}
	}
}
