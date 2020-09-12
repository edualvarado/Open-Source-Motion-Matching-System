﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;

namespace DW_Gameplay
{
    [System.Serializable]
    public struct PoseData
    {
        [SerializeField]
        public BoneData[] bones;

        public int Count 
        { 
            get
            {
                return bones.Length;
            }
            private set
            {
            }
        }

        public PoseData(int count)
        {
            bones = new BoneData[count];
        }

        public PoseData(BoneData[] bones)
        {
            this.bones = bones;
        }

        public BoneData GetBoneData(int index)
        {
            return bones[index];
        }

        public void SetBone(BoneData bone, int index)
        {
            this.bones[index].Set(bone);
        }

        public void SetBone(float3 pos, float3 vel, int index)
        {
            this.bones[index].Set(pos, vel);
        }
        public float CalculateCost(PoseData toPose, PoseCostType type)
        {
            float cost = 0f;
            for (int i = 0; i < Count; i++)
            {
                cost += bones[i].CalculateCost(toPose.GetBoneData(i), type);
            }
            return cost;
        }

        public static void Lerp(ref PoseData buffor, PoseData first, PoseData next, float factor)
        {
            for (int i = 0; i < first.Count; i++)
            {
                buffor.SetBone(BoneData.Lerp(first.GetBoneData(i), next.GetBoneData(i), factor), i);
            }
        }
    }
}
