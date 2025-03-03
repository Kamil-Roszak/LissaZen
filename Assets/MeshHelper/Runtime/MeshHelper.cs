using UnityEngine;

namespace MeshHelper{
    public class MeshHelper
    {
        private const int _defaultPrimitivesResultion = 5;

        /// <summary>
        /// Method that generates simple UV Sphere
        /// </summary>
        /// <param name="numberOfSlices"></param>
        /// <param name="numberOfStacks"></param>
        /// <param name="flipped"></param>
        /// <returns></returns>
        public static Mesh CreateSphere(int numberOfSlices = _defaultPrimitivesResultion,
            int numberOfStacks = _defaultPrimitivesResultion,
            bool flipped = false,
            Vector3 offset = default)
        {
            //Can be optimised, it just quick implementation of:
            //https://danielsieger.com/blog/2021/03/27/generating-spheres.html

            Mesh mesh = new Mesh();
            
            int verticlesCount = numberOfSlices * (numberOfStacks - 1) + 2;
            Vector3[] verticles = new Vector3[verticlesCount];
            int[] triangles = new int[numberOfSlices * 6 + ((numberOfSlices * 6) * (numberOfStacks - 2))];

            //top vertex
            verticles[0] = new Vector3(0, 1, 0) + offset;

            for(int stackIndex = 0; stackIndex < numberOfStacks - 1; stackIndex++)
            {
                var phi = Mathf.PI * (stackIndex + 1) / (numberOfStacks);
                for(int sliceIndex = 0; sliceIndex < numberOfSlices; sliceIndex++)
                {
                    var theta = 2.0f * Mathf.PI * (sliceIndex) / (numberOfSlices);
                    var x = Mathf.Sin(phi) * Mathf.Cos(theta);
                    var y = Mathf.Cos(phi);
                    var z = Mathf.Sin(phi) * Mathf.Sin(theta);
                    verticles[1 + numberOfSlices * stackIndex + sliceIndex] = new Vector3(x, y, z) + offset;
                }
            }

            verticles[verticlesCount - 1] = new Vector3(0, -1, 0) + offset;

            var triangleIndex = 0;
            for(int slicesIndex = 0; slicesIndex < numberOfSlices; slicesIndex++)
            {
                var i0 = slicesIndex + 1;
                var i1 = (slicesIndex + 1) % numberOfSlices + 1;

                if(flipped)
                {
                    triangles[triangleIndex] = i1;
                    triangles[triangleIndex + 1] = 0;
                    triangles[triangleIndex + 2] = i0;
                }
                else
                {
                    triangles[triangleIndex] = 0;
                    triangles[triangleIndex + 1] = i1;
                    triangles[triangleIndex + 2] = i0;
                }
                
                triangleIndex += 3;

                i0 = slicesIndex + numberOfSlices * (numberOfStacks - 2) + 1;
                i1 = (slicesIndex + 1) % numberOfSlices + numberOfSlices * (numberOfStacks - 2) + 1;

                if (flipped)
                {
                    triangles[triangleIndex] = i0;
                    triangles[triangleIndex + 1] = verticlesCount - 1;
                    triangles[triangleIndex + 2] = i1;
                }
                else
                {   
                    triangles[triangleIndex] = verticlesCount - 1;
                    triangles[triangleIndex + 1] = i0;
                    triangles[triangleIndex + 2] = i1;
                }
                triangleIndex += 3;
            }
            
            for (int stackIndex = 0; stackIndex < numberOfStacks - 2; stackIndex++)
            {
                var j0 = stackIndex * numberOfSlices + 1;
                var j1 = (stackIndex + 1) * numberOfSlices + 1;
                for(int sliceIndex = 0; sliceIndex < numberOfSlices; sliceIndex++)
                {
                    var i0 = j0 + sliceIndex;
                    var i1 = j0 + (sliceIndex + 1) % numberOfSlices;
                    var i2 = j1 + (sliceIndex + 1) % numberOfSlices;
                    var i3 = j1 + sliceIndex;


                    if (flipped)
                    {
                        triangles[triangleIndex] = i0;
                        triangles[triangleIndex + 1] = i2;
                        triangles[triangleIndex + 2] = i1;
                        triangleIndex += 3;

                        triangles[triangleIndex] = i0;
                        triangles[triangleIndex + 1] = i3;
                        triangles[triangleIndex + 2] = i2;
                        triangleIndex += 3;
                    }
                    else
                    {
                        triangles[triangleIndex] = i0;
                        triangles[triangleIndex + 1] = i1;
                        triangles[triangleIndex + 2] = i2;
                        triangleIndex += 3;

                        triangles[triangleIndex] = i0;
                        triangles[triangleIndex + 1] = i2;
                        triangles[triangleIndex + 2] = i3;
                        triangleIndex += 3;
                    }

                    
                }
            }

            mesh.SetVertices(verticles);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            return mesh;
        }

    }
}
