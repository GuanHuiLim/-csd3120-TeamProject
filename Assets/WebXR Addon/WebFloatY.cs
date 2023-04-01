using Zinnia.Action;
using WebXR;
using Unity;

public class WebFloatY : FloatAction
{
    public WebXRController controller;
    // Update is called once per frame
    void Update()
    {
        Receive(controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).y);
    }
}
