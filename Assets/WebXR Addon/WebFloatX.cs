using Zinnia.Action;
using WebXR;
using Unity;

public class WebFloatX : FloatAction
{
    public WebXRController controller;

    // Update is called once per frame
    void Update()
    {
        Receive(controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).x);
    }
}
