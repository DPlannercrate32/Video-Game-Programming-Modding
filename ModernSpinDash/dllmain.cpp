// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"

// Define a high-resolution clock type for precision
using Clock = std::chrono::high_resolution_clock;
using TimePoint = std::chrono::time_point<Clock>;

// Global variable to store the start time
TimePoint g_StartTime = Clock::now();
boost::shared_ptr<Hedgehog::Sound::CSoundHandle> chargesound;


extern "C" void __declspec(dllexport) OnFrame() {
    int* p;
    
    float* maxspeed = *(float**)0x1E5E2F0;
    float* boostgauge = *(float**)0x1E5E2F0;
    //float* Yaw = reinterpret_cast<float*>(0x1E77AC0);
    float* DriftSensi = reinterpret_cast<float*>(0x1A48BF0);
    byte* holdjump = reinterpret_cast<byte*>(0x1E77B54);
    
    static bool spindashmode = false;
    static bool spindashmode2 = false;
    static bool waitrelease, chargeuncurl = false;
    
    static float timelimit = 2.0f;
    static float tapped, tappedlimit, accelforce, accelimit, preVel, deccelimit, preVel2 = 0.0f;
    static bool boostmode = false;
    static float basespeed = 60.0f;
    static float revspeed, chargespeed, boostmulti, slopeincr, baseslopincr = 0.0f;
    float spdashspeed, adder = 0.0f;
    static bool slide = false;
    static bool jump, slopesp, xzweirdneds, lvlcam = false;
    static bool yawoffon, RstickUp = true;
    static float maxturn = 8.0f;
    static float maxturnup = 5.0f;
    static float turnR, turnL, turnR1, turnL1, turnU, turnU1, turnD, turnD1, tighten, tighten2, Uptighten = 0.0f;
    static float rightturn, leftturn, downturn, upturn, updownLR, tightsteer, UDtightsteer = 0.0f;
    static float boostroll = 100.0f;
    static bool spinnoise, chargenoise = false; 
    static float chrgenoise = 2.0f;
    static bool spindashcharge, spindashrev, spinaccell, spindeccell, holdLR, holdLR2, HVhold = false; 

    // Record the current time
    TimePoint currentTime = Clock::now();

    // Calculate delta time
    std::chrono::duration<double> elapsedTime = currentTime - g_StartTime;
    std::chrono::duration<double, std::milli> elapsedTime2 = currentTime - g_StartTime;
    double deltaTimeInSeconds = elapsedTime.count();
    double deltaTimeInMillisecs = elapsedTime2.count();
    // Update the start time for the next frame
    g_StartTime = currentTime;

    Sonic::Player::CPlayerSpeedContext* context;
    Sonic::Player::CSonicClassicContext* classiccontext;
    auto gameDocument = Sonic::CGameDocument::GetInstance();
    auto camera = gameDocument->GetWorld()->GetCamera();

    context = Sonic::Player::CPlayerSpeedContext::GetInstance();
    classiccontext = Sonic::Player::CSonicClassicContext::GetInstance();
     
    

    p = *(int**)0x1E5E2F0;
    if (context != NULL && p != NULL && maxspeed != NULL && camera != NULL && DriftSensi != NULL && classiccontext == NULL) {
       /**/ const uint32_t result = *(uint32_t*)((uint32_t) * (void**)((uint32_t) * &context + 0x110) + 172);
        if (!result)
            return;
        float* position = (float*)(*(uint32_t*)(result + 16) + 112);
        float* velocity = (float*)(result + 656);
        float Vel = sqrtf(velocity[0] * velocity[0] + velocity[2] * velocity[2]);
        float incr = Vel / 100.0f;
        //*Yaw = 50.0f;
        //*DriftSensi = 850.0f;
        WRITE_MEMORY(0xDF2B59 + 1, int32_t, -1);
        
        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_MaxVelocityFinalMax] = 145.0f;     
        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_SlidingMinTime] = 500.0f;
        
        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFailureAddVelocity] = 0.0f;
        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftSuccessAddVelocity] = 0.0f;
        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftDecVelocityFirst] = 0.0f;
        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftDecVelocityFirstLowSpeed] = 0.0f;
        //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFinishVelocity] = 0.0f;
        

        if (context->m_Is2DMode == false){
            if(spinaccell == true)
               accelforce = 25.0f;
            else
               accelforce = 10.1f;
            if (boostmode == true && slide == true)
                context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_BoostAccelerationForce] = 12.1f;
            else
                context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_BoostAccelerationForce] = 37.5f;
            context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_SlidingBrakeForce] = 0.0f;
        }else{
            accelforce = 10.5f;
            if (boostmode == true && slide == true)
                context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_BoostAccelerationForce] = 12.5f;
            else
                context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_BoostAccelerationForce] = 22.5f;
            context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_SlidingBrakeForce] = 1.0f;
            }
        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_AccelerationForce] = accelforce;

        if(spindashmode == false && spindashmode2 == false)
            context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftLowSpeedVelocityThreshold] = velocity[0] + velocity[2];
        if (context->m_Is2DMode == false) {
            if (*(maxspeed + 0x53C / 4) == 60.0f) {
                basespeed = 60.0f;
                boostmulti = 1.4f;
            }
            if (*(maxspeed + 0x53C / 4) == 70.0f) {
                basespeed = 70.0f;
                boostmulti = 1.286f;
            }
        }            
        else {
            if (*(maxspeed + 0x53C / 4) == 30.0f) {
                basespeed = 30.0f;
                boostmulti = 1.4f;
            }
            if (*(maxspeed + 0x53C / 4) == 35.0f) {
                basespeed = 35.0f;
                boostmulti = 1.5f;
            }
        }
        
        if (context->m_pPlayer->m_StateMachine.GetCurrentState() != NULL) {
            if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Goal" && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "GoalAir" && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "NormalDamageDead" && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "NormalDamage" && context->StateFlag(eStateFlag_ReadyGoOpened) == false && context->StateFlag(eStateFlag_Pause) == false) {

                if (boostmode == true || spindashmode == true || spindashmode2 == true || context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Drift")
                    setCollision(TypeSonicBoost, true);
                else
                    setCollision(TypeSonicBoost, false);
                //-------------------------------------Speed Manipulation------------------------------------------------------------------------------------------------------
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Vel > (basespeed * boostmulti) + 1.0f) {
                    slopesp = true;
                    slopeincr = Vel - ((basespeed * boostmulti));
                    baseslopincr = slopeincr + ((basespeed * boostmulti) - basespeed);
                }

                if (slopesp == true && Vel < basespeed - 12.0f) {
                    slopesp = false;
                    slopeincr = 0.0f;
                    baseslopincr = 0.0f;
                }
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && velocity[1] >= -0.5f && velocity[1] < 10.1f)
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_BoostStartVelocityRate] = 0.75f;
                else
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_BoostStartVelocityRate] = 1.2f;
                /*if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && yawoffon == false)
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_RotationForceRateMaxLw] = 25.0f;
                else
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_RotationForceRateMaxLw] = 1.0f;*/

                if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_X) == true && *(boostgauge + 0x5BC / 4) > 0.0f) {
                    if (basespeed == 70.0f && context->m_Is2DMode == false)
                        *(maxspeed + 0x53C / 4) = ((basespeed * boostmulti) - 0.02f) + slopeincr;
                    else if (basespeed == 35.0f && context->m_Is2DMode == true)
                        *(maxspeed + 0x53C / 4) = (basespeed * boostmulti) + slopeincr;
                    else if (basespeed == 60.0f && context->m_Is2DMode == false)
                        *(maxspeed + 0x53C / 4) = (basespeed * boostmulti) + slopeincr;
                    else if (basespeed == 30.0f && context->m_Is2DMode == true)
                        *(maxspeed + 0x53C / 4) = (basespeed * boostmulti) + slopeincr;
                    boostmode = true;
                }
                else {
                    boostmode = false;
                    *(maxspeed + 0x53C / 4) = basespeed + baseslopincr;
                }

                if (boostmode == true && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && velocity[1] < 0.0f) {
                    if (context->m_Is2DMode == false)
                        boostroll = 205.0f;
                    else
                        boostroll = 200.0f;

                }
                else if (context->m_Is2DMode == false) {
                    boostroll = 118.0f;
                }
                else
                    boostroll = 100.0f;

                if ((context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" || context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Drift") && velocity[1] < 0.0f && Vel < (basespeed * 1.4f) + 10.0f)
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_Gravity] = boostroll;
                else
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_Gravity] = 35.000f;


                context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_WallJumpReadyGravity] = 0.0f;
                if (spindashcharge == true && chrgenoise == 2.0f) {
                    chargenoise = true;
                }
                if (chargenoise == true && chrgenoise > 0.0f)
                    chrgenoise -= deltaTimeInSeconds;
                if (chrgenoise <= 0.0f) {
                    chargenoise = false;
                    chrgenoise = 2.0f;
                }
                //---------------------------------Spindash Charging-------------------------------------------------------------------------------------------------------------------------
                                //As long as I'm holding both buttons the speed gets reset
                if (spindashrev == false && context->GetCurrentAnimationName() != "Squat" && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_B) && (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightTrigger) || Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftTrigger)))
                    spindashcharge = true;
                if (spindashcharge == false && sqrtf(velocity[0] * velocity[0] + velocity[1] * velocity[1] + velocity[2] * velocity[2]) == 0.0f && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_B) && context->m_Grounded == true) {
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Drift")
                        context->ChangeAnimation("Squat");

                }
                if (context->GetCurrentAnimationName() == "Squat" && (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightTrigger) == true || Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftTrigger) == true) && spindashcharge == false)
                    spindashrev = true;
                if ((spindashrev == true || spindashcharge == true) && context->m_Grounded == true) {
                    //if (spindashcharge == false && sqrtf(velocity[0] * velocity[0] + velocity[1] * velocity[1] + velocity[2] * velocity[2]) == 0.0f && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_B) && (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightTrigger) == false && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftTrigger) == false)) {
                        //spindashrev = true;
                        
                    //}
                    //if (spindashrev == false && context->GetCurrentAnimationName() != "Squat" && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_B) && (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightTrigger) || Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftTrigger)))
                        //spindashcharge = true;
                    //context->SetYawRotation(90.0f);
                    spinaccell = true;             
                    spindashmode = true;
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFrontAccelForceRateLowSpeed] = 0.0f;
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftAngleReturnForceLowSpeed] = 0.0f;
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftLowSpeedVelocityThreshold] = 300.0f;
                    //if (Vel > 1.0f && (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightBumper) || Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftBumper)))
                        //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = 100.0f;
                    if (context->m_Is2DMode == false) {
                        if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStick)) {
                            tighten = 25.0f;
                            tighten2 = 5.0f;
                            *DriftSensi = 2550.0f;
                        }
                        else if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightBumper) || Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftBumper)) {
                            tighten = 1.0f;
                            tighten2 = 1.0f;
                            *DriftSensi = 850.0f;
                        }
                        else {
                            tighten = 2.45f;
                            tighten2 = 1.0f;
                            *DriftSensi = 850.0f;
                        }
                        if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical > 0.50f)
                            Uptighten = 0.45f;
                        else
                            Uptighten = 1.0f;
                        if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal > 0.0f)
                            context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal * tighten) * Uptighten;
                        else
                            context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = ((Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal / tighten2) * tighten) * Uptighten;


                        if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_Y))
                            chargespeed = 0.0f;

                    }
                    else
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = 0.0f;
                    if (spinnoise == false) {
                        if (spindashrev == true)
                            context->PlaySound(2002108, true);
                        if (chrgenoise == 2.0f)
                            context->PlaySound(2002109, spindashcharge);
                        //chargesound = context->PlaySound(2002109, spindashcharge);
                        chargesound = NULL;
                        spinnoise = true;
                    }
                    context->ChangeState("Drift");

                }
                else {
                    if (spindashmode == false)
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFrontAccelForceRateLowSpeed] = 1.7f;
                    if (context->m_Is2DMode == true)
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = 5.0f;
                    else
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = 2.0f;
                    //if(spindashmode == false)
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFrontAccelForceRate] = 1.5f;
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftAngleReturnForceLowSpeed] = 3.5f;
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftLowSpeedVelocityThreshold] = 60.1f;
                    *DriftSensi = 850.0f;
                }

                if (spindashmode == true) {
                    if (spindashcharge == false && (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_LeftTrigger) == true || Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_RightTrigger) == true) && revspeed < 200.0f) {
                        revspeed += 20.0f;
                        context->PlaySound(2002108, true);
                    }
                    if (spindashcharge == true && chargespeed < 200.0f)
                        chargespeed += deltaTimeInMillisecs * 0.122f;
                }

                if (spindashcharge == true && Vel > 0.5f)
                    context->AddVelocity(-context->GetHorizontalMovementDirection());

                if (revspeed > 0) {
                    if (revspeed > 60.0f)
                        revspeed -= deltaTimeInSeconds * 60.0f;
                }

                spdashspeed = (basespeed / 4) + revspeed + (chargespeed + 8.0f);

                if (context->m_Is2DMode == true) {
                    if (spindashcharge == true)
                        adder = 94.0f;
                    else
                        adder = 105.0f;
                }
                else
                    adder = 29.0f;
                
//---------------------------------------------------------When the spindash is released-------------------------------------------------------------------------
                if (spindashmode == true && Sonic::CInputState::GetInstance()->GetPadState().IsReleased(Sonic::eKeyState_B)) {
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFrontAccelForceRateLowSpeed] = spdashspeed + adder;  
                    if (spindashcharge == true && Vel > 9.0f && Vel < (basespeed * boostmulti) - 8.3f && context->m_Is2DMode == false)
                        context->AddVelocity(context->GetHorizontalMovementDirection() * ((spdashspeed + adder) * 0.23f));
                    else if(spindashcharge == true && Vel > 9.0f && Vel < (basespeed * boostmulti) - 8.3f && context->m_Is2DMode == true)
                        context->AddVelocity(context->GetHorizontalMovementDirection() * ((spdashspeed + adder) * 0.11f));
                    //context->ChangeState("Drift");
                    if (spindashrev == true) {
                        if (spdashspeed + adder > 220.0f) {
                            context->PlaySound(2002055, true);
                            context->PlaySound(2002055, true);
                        }
                        else
                            context->PlaySound(2002111, true);
                    }
                    if (spindashcharge == true) {
                        if (spdashspeed + adder > 220.0f) {
                            context->PlaySound(2002110, true);
                            context->PlaySound(2002110, true);
                        }
                        else
                            context->PlaySound(2002110, true);
                    }

                    spindashmode = false;
                    spindashmode2 = true;
                    spinnoise = false;
                    spindashrev = false;
                    spindashcharge = false;
                    
                }
                chargesound.reset();
                //if (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_Y))
                    //chargesound.reset();
                if (spindashmode2 == true && velocity[1] == 0.0f) {
                    context->ChangeState("Drift");

                }
                if (spinaccell == true) {
                    accelimit += deltaTimeInSeconds;
                    if (accelimit > 0.5f) {
                        preVel = Vel;
                        accelimit = 0.0f;
                    }
                    if (Vel - preVel < 0.0f) {
                        spinaccell = false;
                        preVel = 0.0f;
                        accelimit = 0.0f;
                    }

                }
                if (spindeccell == true) {
                    deccelimit += deltaTimeInSeconds;
                    if (deccelimit > 0.5f) {
                        preVel2 = Vel;
                        deccelimit = 0.0f;
                    }
                    if (Vel - preVel2 < 0.0f && slide == true){// && velocity[1] >= -0.5f && velocity[1] < 3.0f) {
                        spindeccell = false;
                        preVel2 = 0.0f;
                        deccelimit = 0.0f;
                        //if(boostmode == true)
                            //context->AddVelocity(context->GetHorizontalMovementDirection() * 5.5f);
                    }

                }

                if (context->m_Is2DMode == false) {
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Drift" && Vel > 5.0f && spindashmode2 == true) {
                        if(context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Goal")
                            context->ChangeState("Sliding");
                        spindashmode2 = false;
                        revspeed = 0.0f;
                        chargespeed = 0.0f;
                        chargeuncurl = true;
                        //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftLowSpeedVelocityThreshold] = velocity[0] + velocity[1];
                    }

                }
                else {
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Drift" && Vel > 5.0f && spindashmode2 == true) {
                        context->ChangeState("Sliding");
                        spindashmode2 = false;
                        revspeed = 0.0f;
                        chargespeed = 0.0f;
                        chargeuncurl = true;
                        //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftLowSpeedVelocityThreshold] = velocity[0] + velocity[1];
                    }
                }


//--------------------------------------------------------Slide Mode Triggering------------------------------------------------------------------------------------------
                if (context->m_Is2DMode == false && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "StartDash" && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_B) == false && Vel > 3.2f && (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_RightTrigger) || Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_LeftTrigger)))
                    context->ChangeState("Drift");
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Drift" && Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_B))
                    context->ChangeState("Sliding");
                if(slide == false && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Walk" && (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_B) || Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_LeftStick)) && context->m_Grounded == true && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Sliding" && Vel > 0.0f)
                    context->ChangeState("Sliding");
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && slide == true)
                    tapped += deltaTimeInSeconds;
                if ((context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Sliding" && Vel < basespeed) || context->m_Grounded == false)
                    chargeuncurl = false;
                if (chargeuncurl == true)
                    tappedlimit = 0.0f;
                else
                    tappedlimit = 500.23f;
                if (slide == false || tapped > 30.0f || Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_B))
                    tapped = 0.0f;
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && slide == true && Sonic::CInputState::GetInstance()->GetPadState().IsReleased(Sonic::eKeyState_B) && tapped > tappedlimit && tapped < tappedlimit + 0.3f) {
                    slide = false;
                    context->ChangeState("SlidingEnd");
                    context->ChangeAnimation("Walk");
                    context->ChangeState("Stand");
                    tapped = 0.0f;
                    chargeuncurl = false;
                }
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding")
                    slide = true;
                if ((Sonic::CInputState::GetInstance()->GetPadState().IsReleased(Sonic::eKeyState_B) || Sonic::CInputState::GetInstance()->GetPadState().IsReleased(Sonic::eKeyState_LeftStick)) &&  slide == true)
                    chargeuncurl = true;
 
                if (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_A) || Vel < 0.5f)
                    slide = false;
                if (slide == true && (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "JumpSpring" || context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "QuickStep" || context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "RunQuickStep" || context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Drift" || context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "HangOn"))
                    slide = false;
                if (slide == true && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Sliding" && context->m_Grounded == false && (velocity[1] < -3 || velocity[1] > 0.0f)) {
                    slide = false;
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "AdlibTrick" && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Fall") {
                        context->ChangeState("Jump");
                    }
                    jump = true;
                }

                if (jump == true && context->m_Grounded == false && Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_A) == true && (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Jump" || context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Fall")) {
                    context->ChangeState("JumpShort");
                    context->ChangeState("Jump");
                    jump = false;
                }
                if (context->m_Grounded == true)
                    jump = false;
                //else
                    //jump = false;

                if (slide == true && (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_RightStick) || Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_Y))) {
                    slide = false;
                    context->ChangeState("Walk");
                }

                if (slide == true && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Sliding" && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() != "Goal" && context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Walk") {
                    context->ChangeState("Sliding");
                    
                }
                if (slide == true)
                    spindeccell = true;
                //if (slide == true && context->m_Grounded == false && (velocity[1] < -3 || velocity[1] > 0.0f)) {
                    //context->ChangeState("Jump");
                    //slide = false;
                //}

                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Vel > 0.0f && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_B) /* && context->m_Is2DMode == false*/ && tapped > 0.24f)
                    context->AddVelocity(-context->GetHorizontalMovementDirection());
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Drift" && (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickLeft) || Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickRight)) == false) {
                    //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocity] = 9.5f;
                    if (context->m_Is2DMode == false && spindashmode == false) {
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = 1.0f;
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocity] = 1.0f;
                    }
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFinishSideVelocity] = 1.0f;
                }
                else {
                    if(context->m_Is2DMode == false && spindashmode == false)
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = 2.0f;
                    //else
                        //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocityLowSpeed] = 5.0f;
                    if(spindashmode == false)
                        context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftMaxAngleVelocity] = 3.2f;
                  
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftSideAngleForce] = 2.7f;
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicParameter_DriftFinishSideVelocity] = 1.31f;
                }
                //if (Vel > 15.0f && (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_LeftTrigger) || Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_RightTrigger)) == true && context->m_Grounded == true && context->m_Is2DMode == false)
                    //context->ChangeState("Drift");
                if (((abs(position[0] - camera->m_Position.x()) > 4.5f || abs(position[2] - camera->m_Position.z()) > 4.5f) ||  abs(position[1] - camera->m_Position.y()) > 2.7f))// || (abs(position[0] - camera->m_Position.x()) > 7.1f || abs(position[2] - camera->m_Position.z()) > 7.1f))// || (abs(position[1] - camera->m_Position.y()) > 2.71f))
                    lvlcam = true;
                else
                    lvlcam = false;
      //-----------------------------------------------------------------------Rolling Movement------------------------------------------------------------------------------------------

                if (context->m_Is2DMode == false) {
                    //X>Z Velocity
                    if (abs(abs(camera->m_MyCamera.m_Direction.x()) - abs(camera->m_MyCamera.m_Direction.z())) < 0.1f) {
                        maxturnup = 2.0f;
                        maxturn = 3.0f;
                    }
                    else {
                        maxturnup = 5.0f;
                        maxturn = 8.0f;
                    }
                    //if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStick))
                        //tightsteer = 0.575f;
                    //else
                    if (boostmode == false)
                        tightsteer = 1.95f;
                    else if(boostmode == true && abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical) < 0.35f)
                        tightsteer = 3.5f;
                    else if (boostmode == true && abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical) > 0.35f)
                        tightsteer = 1.95f;

                    if (abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical) > 0.8374f)
                        UDtightsteer = 0.35f;
                    else
                        UDtightsteer = 1.0f;

                    if (abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal) > 0.02f && abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical) > 0.02f)
                        HVhold = true;
                    else
                        HVhold = false;

                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickRight) == true) {

                        //if (abs(velocity[2]) < abs(velocity[0]) * 0.5f)
                            //context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, 'x', "R", incr));

                        if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal > 0.99f) {
                            if (turnR < maxturn)
                                turnR += 0.1f;
                            //if (turnL > 0.0f)
                            //turnL, turnL1, turnR1, turnU, turnD = 0.0f;
                            //context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, "R", velocity[0], velocity[2], turnR, 0.0f, 0.0f, 0.0f, false));
                        }

                    }
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal > 0.0f) {
                        //if (turnR1 < maxturn)
                            //turnR1 += 0.09f;
                        turnR1 = (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal * 3.5f * tightsteer) * UDtightsteer;
                        //if (turnL1 > 0.0f)
                        //if (Vel > 60.0f)
                            //turnR1 *= 0.5f;
                        //turnL1, turnL, turnR, turnU, turnD = 0.0f;
                        context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, lvlcam,  "R", velocity[0], velocity[2], turnR1, 0.0f, 0.0f, 0.0f, false));
                    }
                    //else
                        //turnR1 = 0.0f;
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickLeft) == true) {

                        //if (abs(velocity[2]) < abs(velocity[0]) * 0.5f)
                            //context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, 'x', "L", incr));
                        if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal < -0.99f) {
                            if (turnL < maxturn)
                                turnL += 0.1f;
                            //if (turnR > 0.0f)
                            //turnR, turnR1, turnL1, turnU, turnD = 0.0f;
                            //context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, "L", velocity[0], velocity[2], 0.0f, turnL, 0.0f, 0.0f, false));
                        }

                    }
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal < 0.0f) {
                        //if (turnL1 < maxturn)
                            //turnL1 += 0.09f;
                        //if (turnR1 > 0.0f)
                        turnL1 = (-Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal * 3.5f * tightsteer) * UDtightsteer;
                        
                        //turnR1, turnR, turnL, turnU, turnD = 0.0f;
                        context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, lvlcam, "L", velocity[0], velocity[2], 0.0f, turnL1, 0.0f, 0.0f, false));
                    }
                    //else
                       // turnL1 = 0.0f;
                    //if (Sonic::CInputState::GetInstance()->GetPadState().IsReleased(Sonic::eKeyState_LeftStickLeft) == true)
                        //turnL = 0.0f;
                    //if (Sonic::CInputState::GetInstance()->GetPadState().IsReleased(Sonic::eKeyState_LeftStickRight) == true)
                        //turnR == 0.0f;
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical > 0.0f) {
                        //if (turnU < maxturnup)
                            //turnU += 1.0f;

                        //if (abs(abs(camera->m_MyCamera.m_Direction.x()) - abs(camera->m_MyCamera.m_Direction.z())) < 0.1f)
                           // turnU = Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical * 0.5f;
                        //else
                        //if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical < 1.0f && Vel > 40.0f)
                            //turnU = Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical * tightsteer * (Vel / 8.0f);
                        //else
                            turnU = Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical * tightsteer;
                        if (abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal) > 0.1f)
                            holdLR = true;
                        else 
                            holdLR = false; 
                        if (abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal) > 0.0f)
                            holdLR2 = true;
                        else
                            holdLR2 = false;
                        
                        context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, lvlcam, "U", velocity[0], velocity[2], 0.0f, 0.0f, turnU, 0.0f, HVhold, holdLR, holdLR2));
                        
                    }
                    //else
                        //turnU = 0.0f;

                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding" && Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical < 0.0f) {
                        //if (turnD < maxturnup)
                            //turnD += 1.0f;
                        //if (abs(abs(camera->m_MyCamera.m_Direction.x()) - abs(camera->m_MyCamera.m_Direction.z())) < 0.1f)
                            //turnD = Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical * 0.5f;
                        //else
                        //if (-Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical > -1.0f && Vel > 40.0f)
                            //turnD = -Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical * tightsteer * (Vel / 8.0f);
                        //else
                            turnD = -Sonic::CInputState::GetInstance()->GetPadState().LeftStickVertical * tightsteer;
                        if (abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal) > 0.1f)
                            holdLR = true;
                        else
                            holdLR = false;
                        if (abs(Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal) > 0.0f)
                            holdLR2 = true;
                        else
                            holdLR2 = false;
                        
                        context->AddVelocity(XZAxis(camera->m_MyCamera.m_Direction, lvlcam, "D", velocity[0], velocity[2], 0.0f, 0.0f, 0.0f, turnD, HVhold, holdLR, holdLR2));

                    }
                    //else
                       // turnD = 0.0f;
                    

                }
                else {
                    if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding") {
                        if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickLeft) == true && waitrelease == false) {
                            if (Isholdback(camera->m_MyCamera.m_Direction, "L", velocity[0], velocity[2]) == true)
                                context->AddVelocity(-context->GetHorizontalMovementDirection());
                            else
                                waitrelease = true;
                        }
                        if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickRight) == true && waitrelease == false) {
                            if (Isholdback(camera->m_MyCamera.m_Direction, "R", velocity[0], velocity[2]) == true)
                                context->AddVelocity(-context->GetHorizontalMovementDirection());
                            else waitrelease = true;
                        }
                        if (Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickLeft) == false && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStickRight) == false)
                            waitrelease = false;
                    }
                }
//------------------------------------------------------------------------------Misc & State Testing--------------------------------------------------------------------------------
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Sliding") //&& Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_A))
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_JumpShortReleaseTime] = -0.2f;
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Walk")
                    context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_JumpShortReleaseTime] = 0.14f;
                if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "JumpShort" && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_A))
                    setCollision(TypeSonicUnbeaten, true);
                else
                    setCollision(TypeSonicUnbeaten, false);
                /*if (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_RightStick) && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightStickUp)) {
                    yawoffon = true;
                    RstickUp = true;
                }

                if (Sonic::CInputState::GetInstance()->GetPadState().IsTapped(Sonic::eKeyState_RightStick) && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_RightStickDown)) {
                    yawoffon = false;
                    RstickUp = false;
                }*/

                if (context->m_Grounded == false)
                    yawoffon = false;
                else
                    yawoffon = true;
                context->SetStateFlag(Sonic::Player::CPlayerSpeedContext::EStateFlag::eStateFlag_UpdateYawByVelocity, yawoffon);



                if (context->GetCurrentAnimationName() == "Sliding" || context->GetCurrentAnimationName() == "SlidingL" || context->GetCurrentAnimationName() == "SlidingR" || context->GetCurrentAnimationName() == "SlidingM") {
                    context->ChangeAnimation("JumpBall");                
                }

                //if(context->StateFlag(eStateFlag_OnNoWallWalkGround))
                    //*(p + 0x5B8 / 4) = 91;

                if ((context->StateFlag(eStateFlag_OnSurfaceWater) || context->StateFlag(eStateFlag_OnAboveWater)) && Sonic::CInputState::GetInstance()->GetPadState().IsDown(Sonic::eKeyState_LeftStick) == false && slide == true && Vel > 40.0f) {
                    //*(p + 0x5B8 / 4) = 91;
                    slide = false;
                    context->ChangeState("Walk");
                }
                
                //else
                   // *(p + 0x5B8 / 4) = 90;

                /*if (abs(abs(camera->m_MyCamera.m_Direction.x()) - abs(camera->m_MyCamera.m_Direction.z())) < 0.1f)
                    *(p + 0x5B8 / 4) = 91;
                else
                    *(p + 0x5B8 / 4) = 90;*/

                    //if (abs(camera->m_MyCamera.m_Direction.x()) > abs(camera->m_MyCamera.m_Direction.z()))
                        //*(p + 0x5B8 / 4) = 91;
                    //else
                        //*(p + 0x5B8 / 4) = 90;
                    /*if (spindashrev == true && spindashcharge == false)
                        *(p + 0x5B8 / 4) = 90;
                    if (spindashrev == false && spindashcharge == true)
                        *(p + 0x5B8 / 4) = 91;
                    if(spindashrev == false && spindashcharge == false)
                        *(p + 0x5B8 / 4) = 89;*/

                    //else
                        //setCollision(TypeNoAttack, false);
                    //if(context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "WallJumpReady")
                        //*(p + 0x5B8 / 4) = 91;
                    //if(context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Walk" && velocity[0] != 0.0f && (abs(velocity[0]) > 0.0f || abs(velocity[2]) > 0.0f))
                        //*(p + 0x5B8 / 4) = 91;
                    //if(Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal > -0.90f)
                        //*(p + 0x5B8 / 4) = 91;
                    //if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal < -0.90f)
                        //*(p + 0x5B8 / 4) = 92;
                    //if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal < 0.90f)
                        //*(p + 0x5B8 / 4) = 98;
                    //if (Sonic::CInputState::GetInstance()->GetPadState().LeftStickHorizontal > 0.90f)
                        //*(p + 0x5B8 / 4) = 100;

                    //if (camera->m_MyCamera.m_Direction.x()> 0.0f)
                        //*(p + 0x5B8 / 4) = 101;
                    //if (camera->m_MyCamera.m_Direction.y() < -0.2f)
                        //*(p + 0x5B8 / 4) = 102;
                    //else
                        //*(p + 0x5B8 / 4) = 101;
                /*if (abs(position[1] - camera->m_Position.y()) > 2.7f)
                    *(p + 0x5B8 / 4) = 101;
                else
                    *(p + 0x5B8 / 4) = 100;*/
                //if (abs(position[0] - camera->m_Position.x()) > 7.1f || abs(position[2] - camera->m_Position.z()) > 7.1f)
                    //*(p + 0x5B8 / 4) = 101;
                //else
                    //*(p + 0x5B8 / 4) = 100;
                 
                   
                    //if (camera->m_MyCamera.m_Direction.z() > 0.0f)
                        //*(p + 0x5B8 / 4) = 103;
                    //if (camera->m_MyCamera.m_Direction.z() < 0.0f)
                        //*(p + 0x5B8 / 4) = 104;
                        //if (context->GetHorizontalVelocityDirection().x() > 0.0f)
                            //*(p + 0x5B8 / 4) = 101;
                        //if (context->GetHorizontalVelocityDirection().x() < 0.0f)
                            //*(p + 0x5B8 / 4) = 102;
                        //if (context->GetHorizontalVelocityDirection().z() > 0.0f)
                            //*(p + 0x5B8 / 4) = 103;
                        //if (context->GetHorizontalVelocityDirection().z() < 0.0f)
                            //*(p + 0x5B8 / 4) = 104;
                        //if (camera->m_MyCamera.m_Direction.z() < 0.0f)
                            //*(p + 0x5B8 / 4) = 101;
                        //if (camera->m_MyCamera.m_Direction.z() > 0.0f)
                            //*(p + 0x5B8 / 4) = 102;
                        //if (camera->m_MyCamera.m_Direction.x() > 0.0f)
                            //*(p + 0x5B8 / 4) = 103;
                       // if (camera->m_MyCamera.m_Direction.x() < 0.0f)
                           // *(p + 0x5B8 / 4) = 104;
                        //if(velocity[2] < 0.0f)
                            //*(p + 0x5B8 / 4) = 100;
                        //else
                            //*(p + 0x5B8 / 4) = 101;
                        //if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Walk")
                            //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_Gravity] = 200.0f;
                        //else
                            //context->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_Gravity] = 35.5f;
                        //if (context->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Walk" && sqrtf(velocity[1] * velocity[1]) > 40.0f)
                               //*(p + 0x5B8 / 4) = 90;
            }
        }
    }
    if (classiccontext != NULL){
        if (classiccontext->m_pPlayer->m_StateMachine.GetCurrentState() != NULL) {
            const uint32_t result2 = *(uint32_t*)((uint32_t) * (void**)((uint32_t) * &classiccontext + 0x110) + 172);
            if (!result2)
                return;
            float* position2 = (float*)(*(uint32_t*)(result2 + 16) + 112);
            float* velocity2 = (float*)(result2 + 656);
            float Vel = sqrtf(velocity2[0] * velocity2[0] + velocity2[2] * velocity2[2]);
            classiccontext->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicClassicParameter_SpinMinTime] = 1000.0f;
            //classiccontext->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicClassicParameter_SpinTimeOfKeeprunning] = 0.0f;
            classiccontext->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::eSonicClassicParameter_SpinVelocityWithChargeLv0] = 13.0f;
            if (classiccontext->m_pPlayer->m_StateMachine.GetCurrentState()->GetStateName() == "Spin" && velocity2[1] < 0.0f)
                classiccontext->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_Gravity] = 66.0f;
            else {
                if (classiccontext->StateFlag(eStateFlag_OnWater))
                    classiccontext->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_Gravity] = 8.8f;
                else
                    classiccontext->m_spParameter->m_scpNode->m_ValueMap[Sonic::Player::ePlayerSpeedParameter_Gravity] = 33.0f;
            }
            //classiccontext->ChangeState("FloatingBoost");
        }
    }

 }


BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
  
    //OnFrame();
    
    return TRUE;
    
}

