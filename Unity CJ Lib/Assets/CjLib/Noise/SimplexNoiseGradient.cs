﻿/******************************************************************************/
/*
  Project - Unity CJ Lib
            https://github.com/TheAllenChou/unity-cj-lib
  
  Author  - Ming-Lun "Allen" Chou
  Web     - http://AllenChou.net
  Twitter - @TheAllenChou
*/
/******************************************************************************/

using UnityEngine;

namespace CjLib
{
  public class SimplexNoiseGradient
  {
    private static bool s_simplexGradientInit = false;
    private static ComputeShader s_simplexGradient;
    private static int s_simplexGradient2KernelId;
    private static int s_simplexGradient3KernelId;
    private static void InitSimplex()
    {
      NoiseCommon.InitCsId();

      if (s_simplexGradientInit)
        return;

      s_simplexGradient = (ComputeShader) Resources.Load("SimplexNoiseGradientCs");
      s_simplexGradient2KernelId = s_simplexGradient.FindKernel("SimplexGradient2");
      s_simplexGradient3KernelId = s_simplexGradient.FindKernel("SimplexGradient3");
    }

    private static void GetSimplex2(out ComputeShader shader, out int kernelId)
    {
      InitSimplex();
      shader = s_simplexGradient;
      kernelId = s_simplexGradient2KernelId;
    }

    private static void GetSimplex3(out ComputeShader shader, out int kernelId)
    {
      InitSimplex();
      shader = s_simplexGradient;
      kernelId = s_simplexGradient3KernelId;
    }

    public static void Compute(Vector2[] output, float scale, float offset, int numOctaves, float octaveOffsetFactor, int seed = 0)
    {
      ComputeShader shader;
      int kernelId;
      GetSimplex2(out shader, out kernelId);
      int[] dimension = new int[] { output.GetLength(0), 1, 1 };
      float[] aScale = { scale };
      float[] aOffset = { offset };
      NoiseCommon.Compute(output, shader, kernelId, seed, dimension, sizeof(float) * 2, aScale, aOffset, numOctaves, octaveOffsetFactor);
    }

    public static void Compute(Vector2[,] output, float[] scale, float[] offset, int numOctaves, float octaveOffsetFactor, int seed = 0)
    {
      ComputeShader shader;
      int kernelId;
      GetSimplex2(out shader, out kernelId);
      int[] dimension = new int[] { output.GetLength(0), output.GetLength(1), 1 };
      NoiseCommon.Compute(output, shader, kernelId, seed, dimension, sizeof(float) * 2, scale, offset, numOctaves, octaveOffsetFactor);
    }

    public static void Compute(Vector3[] output, float scale, float offset, int numOctaves, float octaveOffsetFactor, int seed = 0)
    {
      ComputeShader shader;
      int kernelId;
      GetSimplex3(out shader, out kernelId);
      int[] dimension = new int[] { output.GetLength(0), 1, 1 };
      float[] aScale = { scale };
      float[] aOffset = { offset };
      NoiseCommon.Compute(output, shader, kernelId, seed, dimension, sizeof(float) * 3, aScale, aOffset, numOctaves, octaveOffsetFactor);
    }

    public static void Compute(Vector3[,] output, float[] scale, float[] offset, int numOctaves, float octaveOffsetFactor, int seed = 0)
    {
      ComputeShader shader;
      int kernelId;
      GetSimplex3(out shader, out kernelId);
      int[] dimension = new int[] { output.GetLength(0), output.GetLength(1), 1 };
      NoiseCommon.Compute(output, shader, kernelId, seed, dimension, sizeof(float) * 3, scale, offset, numOctaves, octaveOffsetFactor);
    }

    public static void Compute(Vector3[,,] output, float[] scale, float[] offset, int numOctaves, float octaveOffsetFactor, int seed = 0)
    {
      ComputeShader shader;
      int kernelId;
      GetSimplex3(out shader, out kernelId);
      int[] dimension = new int[] { output.GetLength(0), output.GetLength(1), output.GetLength(2) };
      NoiseCommon.Compute(output, shader, kernelId, seed, dimension, sizeof(float) * 3, scale, offset, numOctaves, octaveOffsetFactor);
    }
  }
}
