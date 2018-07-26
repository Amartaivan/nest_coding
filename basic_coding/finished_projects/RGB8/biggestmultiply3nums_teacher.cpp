//O(n^2)
#include <iostream>

using namespace std;

int main()
{

   int n, i, j, k, p, stop, max, maxi, maxj, maxk;
   cin >> n;

   int a[n], b[n], init[n];

   for (i = 0; i < n; i++){
       cin >> a[i];
       init[i] = a[i];
       b[i] = i;
   }
   max = a[0] * a[1] * a[2];
   maxi = 0;
   maxj = 1;
   maxk = 2;
   j = 0;
   do{
       j++;
       stop = 0;
       for (i = 0; i < n - j; i++){
           if (a[i] > a[i + 1] || (a[i] == a[i+1] && b[i] < b[i + 1])){
               stop = 1;
               swap(a[i], a[i + 1]);
               swap(b[i], b[i + 1]);
           }
       }
   } while(stop == 1);

   for (i = 0; i < n - 1; i++){
       for (j = i + 1; j < n; j++){
           
            p = init[i] * init[j];
            if ( p > 0){
                k = n - 1;
                while(b[k] == i || b[k] == j) k--;
            }else{
                k = 0;
                while(b[k] == i || b[k] == j) k++;    
            }
            p = p * a[k];
            if (max < p){
                max = p;
                maxi = i;
                maxj = j;
                maxk = b[k];
                if (maxj > maxk) 
                    swap(maxj, maxk);
                if (maxi > maxj)
                    swap(maxi, maxj);    
            }
       }
   }
   cout << init[maxi] << " " << init[maxj] << " " << init[maxk]<< endl;
   return 0;
}
