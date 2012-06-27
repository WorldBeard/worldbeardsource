using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.DevIl;
using EarthCoreEngine;
using EarthCoreEngine.Input;
using WorldBeardEngine.Components;
using WorldBeardEngine.Logging;

namespace WorldBeardEngine
{
    public partial class Form1 : Form
    {
        bool _fullscreen = false;
        FastLoop _fastLoop;
        StateSystem _system = new StateSystem();
        Input _input = SingletonFactory.GetInput();
        FormData _formData;

        public Form1()
        {
            string assembly_main_dir = @Directory.GetCurrentDirectory();
            ConfigSettings.InitConfigSettings(assembly_main_dir);

            SingletonFactory.LOG.Log("Test string", LoggerLevel.HIGH);
            SingletonFactory.LOG.Log("Testing components", LoggerLevel.MID, ComponentType.Animation);
            SingletonFactory.LOG.Log("This should log", ComponentType.Rendering);
            SingletonFactory.LOG.Log("This should log", ComponentType.Collision);
            SingletonFactory.LOG.Log("This should log", ComponentType.Animation);
            SingletonFactory.LOG.Log("This shouldn't log", ComponentType.Physics);
            
            _formData = SingletonFactory.GetFormData();
            InitializeComponent();
            _openGLControl.InitializeContexts();

            _input.Mouse = new Mouse(this, _openGLControl);
            _input.Keyboard = new Keyboard(_openGLControl);

            InitializeDisplay();
            InitializeDevIl();
            InitializeFonts();
            InitializeGameStates();

            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeGameStates()
        {
            // LOAD GAME STATES AND SET STARTING STATE HERE
            _system.AddState("LevelState", new LevelState("Level01.wbl"));

            _system.ChangeState("LevelState");
        }

        private void InitializeDevIl()
        {
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);
        }

        private void InitializeFonts()
        {
            // LOAD FONTS HERE
        }

        private void GameLoop(double elapsedTime)
        {
            UpdateInput(elapsedTime);
            _system.Update(elapsedTime);
            _system.Render();
            _openGLControl.Refresh();
        }

        private void UpdateInput(double elapsedTime)
        {
            _input.Update(elapsedTime);
        }

        private void InitializeDisplay()
        {
            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new Size(ConfigSettings.STARTING_SCREEN_WIDTH, ConfigSettings.STARTING_SCREEN_HEIGHT);
            }
            Setup2DGraphics(ClientSize.Width, ClientSize.Height); // Set up OpenGL matrices
        }

        // When Form’s size changes, resize OpenGL to match and re-set up OpenGL matrices
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            _formData.Width = this.Width;
            _formData.Height = this.Height;
        }

        // THE FOLLOWING METHODS ARE INTENTIONALLY BLANK
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void _openGLControl_Load(object sender, EventArgs e)
        {
        }
    }
}




