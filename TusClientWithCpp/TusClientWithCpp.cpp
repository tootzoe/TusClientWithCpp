// TusClientWithCpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
 

#include "PEModule.h"
 
#include "LibraryException.h"

  
using namespace std;
using namespace tootzoe::tusCSharpClient;



int main()
{
	  
	try
	{ 

		PEModule lib(_T("TusClient.dll"));
		cout << ".NET TusClient.dll (netfx; C#) is ready for requests:\n\n";
		 

		double x2 = lib.call<double>("AmazingSin", 5, 24.781, (double)11);
		printf_s("  AmazingSin(5, 24.781, 11) == %e\n", x2);


		const char* tmpStr = lib.call<const char*>("GetDomainName",true);
		//printf_s("  GetDomainName(true) == %s\n", tmpStr);
		cout << " GetDomainName(true) == " << tmpStr ;

	 
		bool IsUploaded = lib.call<bool>("TusUploadFile" , "C:/Users/thor/Pictures/livecodingPrinciple.jpg");
		//printf_s("  AmazingSin(5, 24.781, 11) == %e\n", x2);
		

	}
	catch (const LibraryException& ex) {
		fprintf_s(stderr, "Error: [ %d ] %s\n", ex.getError(), ex.what());
		return ERROR_INVALID_DATA;
	}
	catch (const std::exception& ex)
	{
		fprintf_s(stderr, "Error: %s\n", ex.what());
		return ERROR_INVALID_FUNCTION;
	}


    std::cout << "\n\nHello World! this is tootzoe.........2222...\n";

}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
