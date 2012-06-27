using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Rendering;
using WorldBeardEngine.Events;
using WorldBeardEngine.Util;

namespace WorldBeardEngine.Services.GUI
{
    public class GuiService : Service
    {
        private Camera _camera = SingletonFactory.GetCamera();

        private Vector _currentMousePositionInWorldCoordinates;

        private GuiComponent _previouslyMousedOverComponent;

        private bool _isPreviouslyMousedOverComponentAlreadyDown = false;

        public GuiService()
        {
            _validRegisterType = ComponentType.GUI;
        }

        public override void Update(double elapsedTime)
        {
            if (_registrees.Count == 0) { return; }

            UpdateCurrentMousePosition();

            if (SingletonFactory.GetInput().Mouse.LeftPressed && _previouslyMousedOverComponent != null && IsCurrentlyMousedOver(_previouslyMousedOverComponent))
            {
                SingletonFactory.GetEventHub().SendEvent("ChangeTextureCommand", this, EventArgWriteUtil.GetChangeTextureCommandArgs(GuiTextureType.MOUSE_OVER), _previouslyMousedOverComponent);
                SingletonFactory.GetEventHub().FireEvent("OnClick", _previouslyMousedOverComponent);
                _isPreviouslyMousedOverComponentAlreadyDown = false;
            }
            if (!IsCurrentlyMousedOver(_previouslyMousedOverComponent) && !SingletonFactory.GetInput().Mouse.LeftHeld) // If still mousing over same component or holding the mouse, do nothing.
            {
                if (_previouslyMousedOverComponent != null)
                {
                    SingletonFactory.GetEventHub().SendEvent("ChangeTextureCommand", this, EventArgWriteUtil.GetChangeTextureCommandArgs(GuiTextureType.NULL), _previouslyMousedOverComponent);
                    _previouslyMousedOverComponent = null;
                }
                foreach (GuiComponent component in _registrees)
                {
                    if (component.IsClickable && IsCurrentlyMousedOver(component))
                    {
                        _previouslyMousedOverComponent = component;
                        SingletonFactory.GetEventHub().SendEvent("ChangeTextureCommand", this, EventArgWriteUtil.GetChangeTextureCommandArgs(GuiTextureType.MOUSE_OVER), component);
                        break;
                    }
                }
            }

            if (SingletonFactory.GetInput().Mouse.LeftHeld && _previouslyMousedOverComponent != null && !_isPreviouslyMousedOverComponentAlreadyDown)
            {
                _isPreviouslyMousedOverComponentAlreadyDown = true;
                SingletonFactory.GetEventHub().SendEvent("ChangeTextureCommand", this, EventArgWriteUtil.GetChangeTextureCommandArgs(GuiTextureType.MOUSE_DOWN), _previouslyMousedOverComponent);
            }

        }

        private void UpdateCurrentMousePosition()
        {
            _currentMousePositionInWorldCoordinates = PositionUtil.GetWorldPosition(SingletonFactory.GetInput().Mouse.Position);
        }

        private bool IsCurrentlyMousedOver(GuiComponent component)
        {
            if (component == null) { return false; }
            double mouseX = _currentMousePositionInWorldCoordinates.X;
            double mouseY = _currentMousePositionInWorldCoordinates.Y;
            if (mouseX > component.Left && mouseX < component.Right && mouseY < component.Top && mouseY > component.Bottom)
            {
                return true;
            }
            return false;
        }

    }
}
