#include <iostream>
#include <cmath>

using namespace std;

struct Point{
	float x, y;
	int index;
};

float Perimeter(Point A, Point B, Point C){
	float a = sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y));
	float b = sqrt((B.x - C.x) * (B.x - C.x) + (B.y - C.y) * (B.y - C.y));
	float c = sqrt((A.x - C.x) * (A.x - C.x) + (A.y - C.y) * (A.y - C.y));
	float p = a + b + c;
	return p;
}

int main(){
	
	int n, i, j, k;
	cin >> n;
	Point A[n], ar, br, cr;
	float result = 0.0;
	
	for (i = 0; i < n; i++){
		
		Point tmp;
		
		cin >> tmp.x >> tmp.y;
		tmp.index = i;
		
		A[i] = tmp;
	}
	
	for (i = 0; i < n; i++)
		for (j = i + 1; j < n; j++)
			for (k = j + 1; k < n; k++){
				if ((i == 0 && j == 1 && k == 2) || Perimeter(A[i], A[j], A[k]) < result){
					result = Perimeter(A[i], A[j], A[k]);
					ar = A[i];
					br = A[j];
					cr = A[k];
				}
			}
			
	cout << ar.index + 1 << ' ' << br.index + 1 << ' ' << cr.index + 1 << endl;
	return 0;
}
