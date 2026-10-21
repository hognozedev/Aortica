void SetAspect_float(in float BlurAmount, out float X, out float Y)
{
    float aspect = _ScreenParams.x / _ScreenParams.y;
    
    float x_amount = BlurAmount;
    float y_amount = BlurAmount;
    
    if (aspect > 1)
    {
        x_amount /= aspect;
    }
    else
    {
        y_amount *= aspect;
    }

    X = x_amount;
    Y = y_amount;
}