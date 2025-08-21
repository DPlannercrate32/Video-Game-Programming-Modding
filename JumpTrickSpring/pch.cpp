// pch.cpp: source file corresponding to the pre-compiled header

#include "pch.h"

// When you are using pre-compiled headers, this source file is necessary for compilation to succeed.
void* setCollision(CollisionType collisionType, bool enabled)
{
	static void* const pEnableFunc = (void*)0xE65610;
	static void* const pDisableFunc = (void*)0xE655C0;

	__asm
	{
		mov edi, 0x1E5E2F0
		mov edi, [edi]

		mov ecx, collisionType
		mov ecx, [ecx]
		push ecx

		cmp enabled, 0
		je jump

		call[pEnableFunc]
		jmp end

		jump :
		call[pDisableFunc]

			end :
	}
}


Hedgehog::Math::CVector XZAxis(Hedgehog::Math::CVector& direction, bool lvlcam, const char* dpad, float velx, float velz, float turnR, float turnL, float turnU, float turnD, bool HV, bool holdLR, bool holdLR2) {

	float turnx = 0.0f;
	float turnz = 0.0f;
	float vel = sqrt(velx * velx + velz * velz);
	float incr = vel / 100.0f;
	char camdir = 'n';
	float brake = 0.0f;
	static bool xandz, xandz2 = false;
	static float Upintense, wideness, horiturn0, horiturn, horiturn2, horiturn3, horiturn4, horicancel, horicancel2, URlimit, URmulti, xzborder = 0.0f;
	static const char* XZ;


	//if(vel > 40.0f)
		//brake = 0.0f + vel/8.0f;
	//if (abs(abs(direction.x()) - abs(direction.z())) < 0.1f) {
	brake = 0.0f;
	incr = 0.0f;
	//}
	if (abs(abs(direction.x()) - abs(direction.z())) < 0.3f)
		xandz = true;
	else
		xandz = false;



	if (xandz == true)
		horicancel = 0.3f;
	else
		horicancel = 1.0f;

	if (abs(abs(direction.x()) - abs(direction.z())) < 0.233f)
		xandz2 = true;
	else
		xandz2 = false;

	if (xandz2 == true)
		horicancel2 = 0.001f;
	else
		horicancel2 = 0.001f;


	bool camnegpos = false;
	/*if (xandz == true) {
		if (direction.x() < 0.0 && direction.z() < 0.0f)
			XZ = "--";
		if (direction.x() > 0.0 && direction.z() > 0.0f)
			XZ = "++";
		if (direction.x() > 0.0 && direction.z() < 0.0f)
			XZ = "+-";
		if (direction.x() < 0.0 && direction.z() > 0.0f)
			XZ = "-+";
	}*/

	if (direction.x() < 0.0f && abs(direction.x()) > abs(direction.z())) {
		camnegpos = false;
		camdir = 'x';
	}
	else if (direction.x() > 0.0f && abs(direction.x()) > abs(direction.z())) {
		camnegpos = true;
		camdir = 'x';
	}
	else if (direction.z() < 0.0f && abs(direction.z()) > abs(direction.x())) {
		camnegpos = false;
		camdir = 'z';
	}
	else if (direction.z() > 0.0f && abs(direction.z()) > abs(direction.x())) {
		camnegpos = true;
		camdir = 'z';
	}
	/*else if (xandz == true) {
		if (direction.x() < 0.0f && abs(direction.x()) > abs(direction.z())) {
			camnegpos = false;
			camdir = 'x';
		}
		else if (direction.x() > 0.0f && abs(direction.x()) > abs(direction.z())) {
			camnegpos = true;
			camdir = 'x';
		}
		else if (direction.z() < 0.0f && abs(direction.z()) > abs(direction.x())) {
			camnegpos = false;
			camdir = 'z';
		}
		else if (direction.z() > 0.0f && abs(direction.z()) > abs(direction.x())) {
			camnegpos = true;
			camdir = 'z';
		}
	}*/
	else
		return Hedgehog::Math::CVector(0.0f, 0.0f, 0.0f);

	//if (abs(abs(direction.x()) - abs(direction.z())) < 0.9f)
		//camdir = 'x';
	if (xandz == false) {
		//horiturn0 = 0.9f;
		horiturn = 0.7f;
		horiturn2 = 1.5f * (vel / 8.0f);
		horiturn3 = 0.28f;
		if (vel > 10.0f)
			horiturn4 = 0.8f;
		else
			horiturn4 = 0.4f;
		if (vel > 10.0f)
			horiturn0 = 3.0f;
		else
			horiturn0 = 0.8f;
		xzborder = 3.98f;
	}
	else {
		//horiturn0 = 0.9f;
		horiturn = 0.7f;
		horiturn2 = 2.4f * (vel / 8.0f);
		horiturn3 = 0.28f;
		horiturn4 = 0.9f;
		horiturn0 = 3.9f;
		if (xandz2 == false)
			xzborder = 3.98f; // 6.3f;
		else
			xzborder = 6.3f;
	}
	//horiturn0 = 3.0f;
	URlimit = 15.0f;
	URmulti = 1.0f;
	/*if (xandz == true) {
		if (XZ == "++") {
			if (dpad == "U" && velx < 0.0f && velz < 0.0f) {
				turnx = turnU * 0.2f;
				turnz = turnU * 0.2f;
				if (velx > 0.0f)
					turnx = turnU * horiturn2;
				else
					turnx = -turnU * horiturn2;
				if (velz < 0.0f)
					turnz = -turnU * horiturn2;
				else
					turnz = turnU * horiturn2;
			}
		}
		else if (XZ == "--") {
			if (dpad == "U" && velx > 0.0f && velz > 0.0f) {
				turnx = -turnU * 0.2f;
				turnz = -turnU * 0.2f;
				if (velx > 0.0f)
					turnx = turnU * horiturn2;
				else
					turnx = -turnU * horiturn2;
				if (velz < 0.0f)
					turnz = -turnU * horiturn2;
				else
					turnz = turnU * horiturn2;
			}
		}
		else if (XZ == "+-") {
			if (dpad == "U" && velx > 0.0f && velz < 0.0f) {
				turnx = -turnU * 0.2f;
				turnz = turnU * 0.2f;
				if (velx > 0.0f)
					turnx = turnU * horiturn2;
				else
					turnx = -turnU * horiturn2;
				if (velz > 0.0f)
					turnz = turnU * horiturn2;
				else
					turnz = -turnU * horiturn2;
			}
		}
		else if (XZ == "-+") {
			if (dpad == "U" && velx < 0.0f && velz > 0.0f) {
				turnx = turnU * 0.2f;
				turnz = -turnU * 0.2f;
				if (velx > 0.0f)
					turnx = turnU * horiturn2;
				else
					turnx = -turnU * horiturn2;
				if (velz > 0.0f)
					turnz = turnU * horiturn2;
				else
					turnz = -turnU * horiturn2;
			}
		}
	}*/
	if (camdir == 'x') {
		if (dpad == "R" && abs(velx) > abs(velz)) {
			turnz = -0.0f - turnR * horiturn3;
			//if (xandz == false && turnR > 8.0f && ((camnegpos == false && velx < 0.0f) || (camnegpos == true && velx > 0.0f)))
				//turnx = turnR * 1.2f;
		}
		if (dpad == "R" && abs(velz) > abs(velx) && ((camnegpos == false && velz > 0.0f) || (camnegpos == true && velz < 0.0f))) {
			turnz = -turnR * horiturn4;
			if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true))
				turnx = -turnR * horiturn4;
			else if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true))
				turnx = turnR * horiturn4;
		}
		if (dpad == "R" && abs(velz) > abs(velx) && ((camnegpos == false && velz < 0.0f) || (camnegpos == true && velz > 0.0f))) {
			if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true))
				turnx = turnR * horiturn4;
			else if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true))
				turnx = -turnR * horiturn4;
		}

		if (dpad == "L" && abs(velx) > abs(velz)) {
			turnz = 0.0f + turnL * horiturn3;
			//if (xandz == false && turnL > 8.0f && ((camnegpos == false && velx < 0.0f) || (camnegpos == true && velx > 0.0f)))
				//turnx = turnL * 1.2f;
		}
		if (dpad == "L" && abs(velz) > abs(velx) && ((camnegpos == false && velz < 0.0f) || (camnegpos == true && velz > 0.0f))) {
			turnz = turnL * horiturn4;
			if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true))
				turnx = -turnL * horiturn4;
			else if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true))
				turnx = turnL * horiturn4;
		}
		if (dpad == "L" && abs(velz) > abs(velx) && ((camnegpos == false && velz > 0.0f) || (camnegpos == true && velz < 0.0f))) {
			if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true))
				turnx = turnL * horiturn4;
			else if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true))
				turnx = -turnL * horiturn4;

		}

		if (dpad == "U" && abs(velz) > abs(velx) && (abs(velz) - abs(velx) > xzborder)) {// && (abs(velx) < 68.0f && lvlcam == true)) {

			//if (HV == false)// && (((velz < 0.0f || velz > 0.0f) && camnegpos == false) || ((velz > 0.0f || velz < 0.0f) && camnegpos == true)))
				//turnx = -turnU * 1.0f * horicancel;
			//if (HV == true && vel > URlimit)// && vel > 15.0f)
				//turnx = -turnU * URmulti * horicancel;
			//else if(vel > 7.0f)
			//if(horiturn0 > ((abs(velx) / 100.0f) * 3.0f))
			turnx = -turnU * horiturn0;//(horiturn0 - (abs(velx)/100.0f)*3.0f) * horicancel;
			//else
				//turnx = -turnU * 0.01f * horicancel;

			/*if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true)) {

				turnz = turnU * horiturn;// *horicancel;
			}
			if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true)) {

				turnz = -turnU * horiturn;// *horicancel;
			}*/
		}
		if (dpad == "U" && abs(velx) > abs(velz)) {
			if (abs(velz) > 20.0f) {
				if (lvlcam == true && holdLR2 == false)
					Upintense = 0.3f + (((abs(velz) - 20.0f) / 16.0f) / 10.0f);
				else
					Upintense = 0.0f;
				wideness = 6.0f;
			}
			else if (abs(velz) <= 20.0f && abs(velz) > 10.0f) {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			else {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			if ((camnegpos == false && velx > 0.0f) || (camnegpos == true && velx < 0.0f)) {
				turnx = 0 - turnU * horiturn2;
				//if (vel < 30.0f) {
				if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true))
					turnz = 0.0f - turnU * (horiturn2)*horicancel;
				else if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true))
					turnz = 0.0f + turnU * (horiturn2)*horicancel;
				//}
			}

			//else if (holdLR == false && ((velz < -wideness && camnegpos == false) || (velz > wideness && camnegpos == true)))
				//turnz = turnU * Upintense * horicancel2;
			//else if (holdLR == false && ((velz > wideness && camnegpos == false) || (velz < -wideness && camnegpos == true)))
				//turnz = -turnU * Upintense * horicancel2;
		}


		if (dpad == "D" && abs(velz) > abs(velx) && (abs(velz) - abs(velx) > xzborder)) {
			//if (HV == false)// && (((velz < 0.0f || velz > 0.0f) && camnegpos == false) || ((velz > 0.0f || velz < 0.0f) && camnegpos == true)))
				//turnx = 0.0f + turnD * 1.0f *horicancel;
			//if (HV == true && vel > URlimit)// && vel > 15.0f)
				//turnx = turnD * URmulti * horicancel;
			//else if (vel > 7.0f)
			//if(horiturn0 > ((abs(velx) / 100.0f) * 3.0f))
			turnx = turnD * horiturn0; //(horiturn0 - (abs(velx)/100.0f)*3.0f) * horicancel;
			//else
				//turnx = turnD * 0.01f * horicancel;


			/*if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true))
				turnz = turnD * horiturn;// *horicancel;
			if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true))
				turnz = -turnD * horiturn;// *horicancel;*/
		}
		if (dpad == "D" && abs(velx) > abs(velz)) {
			if (abs(velz) > 20.0f) {
				if (lvlcam == true && holdLR2 == false)
					Upintense = 0.3f + (((abs(velz) - 20.0f) / 16.0f) / 10.0f);
				else
					Upintense = 0.0f;
				wideness = 6.0f;
			}
			else if (abs(velz) <= 20.0f && abs(velz) > 10.0f) {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			else {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			if ((camnegpos == false && velx < 0.0f) || (camnegpos == true && velx > 0.0f)) {
				turnx = 0.0f + turnD * horiturn2;
				//if (vel < 30.0f) {
				if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true))
					turnz = 0.0f - turnD * (horiturn2)*horicancel;
				else if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true))
					turnz = 0.0f + turnD * (horiturn2)*horicancel;
				//}
			}
			//else if (holdLR == false && ((velz < -wideness && camnegpos == false) || (velz > wideness && camnegpos == true)))
				//turnz = turnD * Upintense * horicancel2;
			//else if (holdLR == false && ((velz > wideness && camnegpos == false) || (velz < -wideness && camnegpos == true)))
				//turnz = -turnD * Upintense * horicancel2;
		}

	}
	else if (camdir == 'z') {
		if (dpad == "R" && abs(velz) > abs(velx)) {
			turnx = 0.0f + turnR * horiturn3;
			//if (xandz == false && turnR > 8.0f && ((camnegpos == false && velz < 0.0f) || (camnegpos == true && velz > 0.0f)))
				//turnz = turnR * 1.2f;
		}
		if (dpad == "R" && abs(velx) > abs(velz) && ((camnegpos == false && velx < 0.0f) || (camnegpos == true && velx > 0.0f))) {
			turnx = turnR * horiturn4;
			if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true))
				turnz = -turnR * horiturn4;
			else if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true))
				turnz = turnR * horiturn4;
		}
		if (dpad == "R" && abs(velx) > abs(velz) && ((camnegpos == false && velx > 0.0f) || (camnegpos == true && velx < 0.0f))) {
			if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true))
				turnz = turnR * horiturn4;
			else if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true))
				turnz = -turnR * horiturn4;

		}

		if (dpad == "L" && abs(velz) > abs(velx)) {
			turnx = -0.0f - turnL * horiturn3;
			//if (xandz == false && turnL > 8.0f && ((camnegpos == false && velz < 0.0f) || (camnegpos == true && velz > 0.0f)))
				//turnz = turnL * 1.2f;
		}
		if (dpad == "L" && abs(velx) > abs(velz) && ((camnegpos == false && velx > 0.0f) || (camnegpos == true && velx < 0.0f))) {
			turnx = -turnL * horiturn4;
			if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true))
				turnz = -turnL * horiturn4;
			else if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true))
				turnz = turnL * horiturn4;
		}
		if (dpad == "L" && abs(velx) > abs(velz) && ((camnegpos == false && velx < 0.0f) || (camnegpos == true && velx > 0.0f))) {
			if ((velz < 0.0f && camnegpos == false) || (velz > 0.0f && camnegpos == true))
				turnz = turnL * horiturn4;
			else if ((velz > 0.0f && camnegpos == false) || (velz < 0.0f && camnegpos == true))
				turnz = -turnL * horiturn4;

		}

		if (dpad == "U" && abs(velx) > abs(velz) && (abs(velx) - abs(velz) > xzborder)) { // && (abs(velz) < 68.0f && lvlcam == true)) {
			//if (HV == false)// && (((velx < 0.0f || velx > 0.0f) && camnegpos == false) || ((velx > 0.0f || velx < 0.0f) && camnegpos == true)))
				//turnz = -turnU * 1.0f * horicancel;
			//if (HV == true && vel > URlimit)// && vel > 15.0f)
				//turnz = -turnU * URmulti * horicancel;
			//else if (vel > 7.0f)
			//if(horiturn0 > ((abs(velz) / 100.0f) * 3.0f))
			turnz = -turnU * horiturn0; //(horiturn0 - (abs(velz)/100.0f)*3.0f) * horicancel;
			//else
				//turnz = -turnU * 0.01f * horicancel;

			/*if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true)) {

				turnx = -turnU * horiturn;// *horicancel;
			}
			if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true)) {

				turnx = turnU * horiturn;// *horicancel;

			}*/
		}
		if (dpad == "U" && abs(velz) > abs(velx)) {
			if (abs(velx) > 20.0f) {
				if (lvlcam == true && holdLR2 == false)
					Upintense = 0.3f + (((abs(velz) - 20.0f) / 16.0f) / 10.0f);
				else
					Upintense = 0.0f;
				wideness = 6.0f;
			}
			else if (abs(velx) <= 20.0f && abs(velx) > 10.0f) {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			else {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			if ((camnegpos == false && velz > 0.0f) || (camnegpos == true && velz < 0.0f)) {
				turnz = 0.0f - turnU * horiturn2;
				//if (vel < 30.0f) {
				if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true))
					turnx = 0.0f - turnU * (horiturn2)*horicancel;
				else if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true))
					turnx = 0.0f + turnU * (horiturn2)*horicancel;
				//}
			}

			//else if (holdLR == false && ((velx < -wideness && camnegpos == false) || (velx > wideness && camnegpos == true)))
				//turnx = turnU * Upintense * horicancel2;
			//else if(holdLR == false && ((velx > wideness && camnegpos == false) || (velx < -wideness && camnegpos == true)))
				//turnx = -turnU * Upintense * horicancel2;
		}


		if (dpad == "D" && abs(velx) > abs(velz) && (abs(velx) - abs(velz) > xzborder)) {
			//if (HV == false)// && (((velx < 0.0f || velx > 0.0f) && camnegpos == false) || ((velx > 0.0f || velx < 0.0f) && camnegpos == true)))
				//turnz = 0.0f + turnD * 1.0f *horicancel;
			//if (HV == true && vel > URlimit)// && vel > 15.0f)
				//turnz = turnD * URmulti * horicancel;
			//else if (vel > 7.0f)
			//if(horiturn0 > ((abs(velz) / 100.0f) * 3.0f))
			turnz = 0.0f + turnD * horiturn0;// (horiturn0 - (abs(velz) / 100.0f) * 3.0f)* horicancel;
			//else
				//turnz = 0.0f + turnD * 0.01f * horicancel;

			/*if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true))
				turnx = -turnD * horiturn;// *horicancel;
			if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true))
				turnx = turnD * horiturn;// *horicancel;*/
		}
		if (dpad == "D" && abs(velz) > abs(velx)) {
			if (abs(velx) > 20.0f) {
				if (lvlcam == true && holdLR2 == false)
					Upintense = 0.3f + (((abs(velz) - 20.0f) / 16.0f) / 10.0f);
				else
					Upintense = 0.0f;
				wideness = 6.0f;
			}
			else if (abs(velx) <= 20.0f && abs(velx) > 10.0f) {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			else {
				if (lvlcam == true)
					Upintense = 0.2f;
				else
					Upintense = 0.0f;
				wideness = 4.0f;
			}
			if ((camnegpos == false && velz < 0.0f) || (camnegpos == true && velz > 0.0f)) {
				turnz = 0.0f + turnD * horiturn2;
				//if (vel < 30.0f) {
				if ((velx < 0.0f && camnegpos == false) || (velx > 0.0f && camnegpos == true))
					turnx = 0.0f - turnD * (horiturn2)*horicancel;
				else if ((velx > 0.0f && camnegpos == false) || (velx < 0.0f && camnegpos == true))
					turnx = 0.0f + turnD * (horiturn2)*horicancel;
				//}
			}
			//else if (holdLR == false && ((velz < -wideness && camnegpos == false) || (velz > wideness && camnegpos == true)))
				//turnx = turnD * Upintense * horicancel2;
			//else if (holdLR == false && ((velz > wideness && camnegpos == false) || (velz < -wideness && camnegpos == true)))
				//turnx = -turnD * Upintense * horicancel2;
		}

	}

	/*if (abs(abs(direction.x()) - abs(direction.z())) < 0.05f) {
		if (vel > 50.0f) {
			turnx *= 0.2f;
			turnz *= 0.2f;
		}

	}*/

	if (dpad == "R" || dpad == "L") {

		if (camnegpos == false)
			return Hedgehog::Math::CVector(turnx, 0.0f, turnz) * (1.0f + (incr * 2.0f));
		else
			return Hedgehog::Math::CVector(-turnx, 0.0f, -turnz) * (1.0f + (incr * 2.0f));



	}
	else if (dpad == "U" || dpad == "D") {

		if (camnegpos == false)
			return Hedgehog::Math::CVector(turnx, 0.0f, turnz) * (1.0f + (incr * 2.0f));
		else
			return Hedgehog::Math::CVector(-turnx, 0.0f, -turnz) * (1.0f + (incr * 2.0f));



	}
	else
		return Hedgehog::Math::CVector(0.0f, 0.0f, 0.0f);
}

bool Isholdback(Hedgehog::Math::CVector& direction, const char* dpad, float velx, float velz)
{
	static bool holddown = false;
	if (direction.x() < 0.0f && abs(direction.x()) > abs(direction.z())) {
		if ((velz < 0.0f && dpad == "R") || (velz > 0.0f && dpad == "L"))
			return false;
		else
			holddown = false;
		if (velz < 0.0f && dpad == "L" && holddown == false)
			return true;
		if (velz > 0.0f && dpad == "R" && holddown == false)
			return true;
	}
	else if (direction.x() > 0.0f && abs(direction.x()) > abs(direction.z())) {
		if ((velz > 0.0f && dpad == "R") || (velz < 0.0f && dpad == "L"))
			return false;
		else
			holddown = false;
		if (velz > 0.0f && dpad == "L" && holddown == false)
			return true;
		if (velz < 0.0f && dpad == "R" && holddown == false)
			return true;
	}
	else if (direction.z() < 0.0f && abs(direction.z()) > abs(direction.x())) {
		if ((velx > 0.0f && dpad == "R") || (velx < 0.0f && dpad == "L"))
			return false;
		else
			holddown = false;
		if (velx > 0.0f && dpad == "L" && holddown == false)
			return true;
		if (velx < 0.0f && dpad == "R" && holddown == false)
			return true;
	}
	else if (direction.z() > 0.0f && abs(direction.z()) > abs(direction.x())) {
		if ((velx < 0.0f && dpad == "R") || (velx > 0.0f && dpad == "L"))
			return false;
		else
			holddown = false;
		if (velx < 0.0f && dpad == "L" && holddown == false)
			return true;
		if (velx > 0.0f && dpad == "R" && holddown == false)
			return true;
	}
	else
		return false;
}