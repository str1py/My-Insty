   M S B A M L   `   `   `  ����N  JInstagram Assistant, Version=1.0.6.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Instagram_Assistant.SplashScreen Instagram_Assistant.SplashScreen  5   	   7    1Instagram_Assistant.SplashScreen.SplashScreenView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	   ?
viewmodels.clr-namespace:Instagram_Assistant.SplashScreen  5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	     � Title$  My Insty��5
   	   $	��400��6   $	��700��6'      	PageColor#���   64    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.    � 
ResizeMode$ NoResize=�6A   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6$   �� *WindowLoaded+5       � DataContext 5      <  6Instagram_Assistant.SplashScreen.SplashScreenViewModel  5   
   	 � 	Resources	 5        EllipseAnim
 �� TargetProperty t� From	 t� To �� 
TargetName g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  blurAnim O� Value O� KeyTime��%�  &	       &	 �     �� 5   
   .��5      t� $
 Margin�6!   $ 196,-20,196,727X�5   !   $ 196,20,196,72X�68   $ AppIcon��5   !   $ 0:0:1D�5   !    5        5      $ EaseOut=�6"   $ 3��67   $ 4Q�6H   5       5"      �� 5$   
   .��5%      U� $
 Radius�6,   $ 
blurEffect��5&       .\�5'      �� $ 0Q�6'   $ 0:0:3.0��61   6E   5(      5)      5*      .��5+      � .+�5-   
   �� $��
0,0,0,-110q�6   $	��400��6)   $��Center=�65   $��Center=�6P   .+�5.      �� $��My Insty��6   $��Center=�5/      $��	0,30,0,30q�50      $y�Hidden=�51        TextBlockMainStyle#���  52      $��50(�53      �� 54      $��-120,100,0,0q�6    �� Orientation$ 
Horizontal=�6/   .+�55      "  FontAwesome.WPF.ImageAwesome     Icon$ CircleOutlineNotch=�6"      
Foreground
 �����   Spin . $��20��58   "   �� 59      $��15(�5:      $��30��5;      
���������59      �� *SplashScreenText+ �� UpdateSourceTrigger$ PropertyChanged=�5<      5=      �� 5>      $��
-340,0,0,0q�6   .+�5?      �� $	��300��6     SliderStyle#���  6%   {�.   � Maximum$	 100Q�5@   5    � Value 6   �� *	LoadValue+6E   5A      5B      A� 5D   
   -   5         A�Name$ AppIcon��5D      $	��300��6#   $	��300��60   $��200,24,200,76q�6<   ��5E      �� 5F       j� Stretch$ UniformToFill=�6    �� ImageSource$> 8pack://application:,,,/Resources/Images/insta-splash.png��5G       J� Effect 5I      -	  'System.Windows.Media.Effects.BlurEffect	  5J        	  Radius$  50Q�6   ! 	  
KernelType$! Gaussian=�5K      -   5        
blurEffect��5L      " � Triggers" 5N      :� 5O      # :� RoutedEvent$# Ellipse.Loaded��6   .[�5P      �� #��[ 6&   5Q      5S      5U      5V      