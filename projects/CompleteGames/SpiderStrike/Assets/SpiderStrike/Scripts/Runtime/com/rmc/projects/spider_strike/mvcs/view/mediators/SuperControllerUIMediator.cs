﻿/**
 * Copyright (C) 2005-2013 by Rivello Multimedia Consulting (RMC).                    
 * code [at] RivelloMultimediaConsulting [dot] com                                                  
 *                                                                      
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the      
 * "Software"), to deal in the Software without restriction, including  
 * without limitation the rights to use, copy, modify, merge, publish,  
 * distribute, sublicense, and#or sell copies of the Software, and to   
 * permit persons to whom the Software is furn
 * 
 * ished to do so, subject to
 * the following conditions:                                            
 *                                                                      
 * The above copyright notice and this permission notice shall be       
 * included in all copies or substantial portions of the Software.      
 *                                                                      
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,      
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF   
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR    
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.                                      
 */
// Marks the right margin of code *******************************************************************

//--------------------------------------
//  Imports
//--------------------------------------
using UnityEngine;
using com.rmc.projects.spider_strike.mvcs.controller.signals;
using strange.extensions.mediation.impl;
using com.rmc.projects.spider_strike.mvcs.view.ui;
using com.rmc.exceptions;
using com.rmc.projects.spider_strike.mvcs.model.vo;



//--------------------------------------
//  Namespace
//--------------------------------------

namespace com.rmc.projects.spider_strike.mvcs.view
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------
	
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------
	
	
	//--------------------------------------
	//  Class
	//--------------------------------------
	public class SuperControllerUIMediator : Mediator
	{
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		/*
		 * NOTE: According to my tests and
		 * 		http://kendlj.wordpress.com/2014/01/03/unit-testing-strangeioc-mediator/
		 * 
		 * 		We cannot do "mediationBinder.Bind<IControllerUI>().To<VirtualControllerUIMediator>();
		 * 
		 * 		So this is a workaround
		 * 
		 **/
		public IControllerUI view 	{ get; set;}

		
		/// <summary>
		/// Gets or sets the sound play signal.
		/// </summary>
		/// <value>The sound play signal.</value>
		[Inject]
		public SoundPlaySignal soundPlaySignal { get; set;}

		/// <summary>
		/// Gets or sets the turret do move signal.
		/// </summary>
		/// <value>The turret do move signal.</value>
		[Inject]
		public TurretDoMoveSignal turretDoMoveSignal 		{ get; set;}
		
		// PUBLIC
		
		
		// PUBLIC STATIC
		
		// PRIVATE
		
		// PRIVATE STATIC
		/// <summary>
		/// Spin amount per click
		/// </summary>
		private const float _TURRET_ROTATION_PER_CLICK = 30;
		
		//--------------------------------------
		//  Methods
		//--------------------------------------
		/// <summary>
		/// Raises the register event.
		/// </summary>
		public override void OnRegister()
		{
			view.init();
			view.uiInputChangedSignal.AddListener (_onUIInputChangedSignal);
			
		}
		
		/// <summary>
		/// Raises the remove event.
		/// </summary>
		public override void OnRemove()
		{
			view.uiInputChangedSignal.RemoveListener (_onUIInputChangedSignal);
		}
		
		
		/// <summary>
		/// Update this instance.
		/// </summary>
		public void Update()
		{
			
			
		}
		
		
		//	PUBLIC
		
		
		// PRIVATE
		/// <summary>
		/// Resets the game.
		/// 
		/// NOTE: This works great. 
		/// 
		/// NOTE: In a more mature game, you may need to manually place each item again.
		/// 		or put this logic in a more central location
		/// 
		/// </summary>
		public void _doResetGame ()
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		
		
		
		// PRIVATE STATIC
		
		// PRIVATE COROUTINE
		
		// PRIVATE INVOKE
		
		//--------------------------------------
		//  Events
		//--------------------------------------
		/// <summary>
		/// _ons the user interface input signal.
		/// </summary>
		/// <param name="aUIInputType">A user interface input type.</param>
		private void _onUIInputChangedSignal (UIInputVO aUIInputVO)
		{



			if (aUIInputVO.uiInputEventType == UIInputEventType.Down) {


				//KEYDOWN
				switch (aUIInputVO.keyCode) {
					case KeyCode.LeftArrow:
						turretDoMoveSignal.Dispatch (new TurretMoveVO( MoveType.Left, _TURRET_ROTATION_PER_CLICK));
						break;
					case KeyCode.RightArrow:
						turretDoMoveSignal.Dispatch (new TurretMoveVO( MoveType.Right, _TURRET_ROTATION_PER_CLICK));
						break;
					case KeyCode.Space:
						turretDoMoveSignal.Dispatch (new TurretMoveVO( MoveType.FiringStart));
						break;
					case KeyCode.Return:
						soundPlaySignal.Dispatch (new SoundPlayVO (SoundType.BUTTON_CLICK));
						_doResetGame();
						break;
					default:
						#pragma warning disable 0162
						throw new SwitchStatementException();
						break;
						#pragma warning restore 0162
				}

			} else {

				//KEYDOWN
				switch (aUIInputVO.keyCode) {
					case KeyCode.Space:
						turretDoMoveSignal.Dispatch (new TurretMoveVO( MoveType.FiringStop));
						break;
				}


			}
			
			
			
		}
	}
}

