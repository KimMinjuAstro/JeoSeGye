using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Utils
{
   // 0 ~ maxCount까지의 숫자 중 겹치지 않는 n개의 난수
   public static int[] RandomNumbers(int maxCount, int n)
   {
      System.Random random = new System.Random(); // System.Random 사용
      int[] defaults = new int[maxCount];   // 0 ~ (maxCount - 1)까지의 기본 배열
      int[] results = new int[n];           // 결과값 저장하는 배열

      // 배열 전체에 0 ~ (maxCount - 1) 값 순서대로 저장
      for (int i = 0; i < maxCount; i++)
      {
         defaults[i] = i;
      }

      // 필요한 n개의 난수 생성
      for (int i = 0; i < n; i++)
      {
         int index = random.Next(0, maxCount); // System.Random 사용

         results[i] = defaults[index];          // 선택된 인덱스의 값을 결과 배열에 저장
         defaults[index] = defaults[maxCount - 1]; // 선택된 값을 마지막 값과 교환하여 중복 방지

         maxCount--; // 선택 가능한 숫자 범위를 줄임
      }

      return results;
   }
}
