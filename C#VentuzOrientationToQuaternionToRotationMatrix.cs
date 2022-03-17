
public SlimDX.Matrix ToSlimDXMatrix(float x, float y, float z, floatw){

  SlimDX.Quaternion q = new SlimDX.Quaternion();

  q.W = w;  
  q.X = x;
  q.Y = -y;
  q.Z = -z;

  SlimDX.Matrix m = new SlimDX.Matrix();
  SlimDX.Matrix.RotationQuaternion(ref q, out m);

  return m;
}
