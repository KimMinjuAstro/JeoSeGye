using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Utils
{
   // 0 ~ maxCount까지의 숫자 중 겹치지 않는 n개의 난수
   public static int[] RandomNumbers(int maxCount, int n)
   {
      int[] defaluts = new int[maxCount];   // 0 ~ maxCount까지 기본 배열
      int[] results = new int[n];          // 결과값 저장하는 배열
      
      // 배열 전체에 0 ~ maxCount값 순서대로 저장
      for (int i = 0; i< maxCount; i++)
      {
         defaluts[i] = i;
      }

      // 필요한 n개의 난수 생성
      for (int i = 0; i < n; i++)
      {
         int index = Random.Range(0, maxCount);

         results[i] = defaluts[index];
         defaluts[index] = defaluts[maxCount - 1];

         maxCount--;
      }

      return results;
   }
}
