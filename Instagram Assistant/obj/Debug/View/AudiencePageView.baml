   M S B A M L   `   `   `  ����  Instagram Assistant/    )Instagram_Assistant.View.AudiencePageView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5
         	FlashAnim �� 
TargetName �� TargetProperty W� From	 W� To g� AutoReverse g� Duration g� RepeatBehavior  	ColorAnim  SecondColor	 �� To��%}  &	        &	 �     �� 5   
   .��5      W� $ Status��6   $ Opacity�5      $ 1X�5      $	 0.3X�5       . $ 0:0:1D�5      $ Forever��5      5      5      �� 5   
   .��5      �� $ Status��6   $( "Foreground.(SolidColorBrush.Color)�5      $ Forever��5       . $ 0:0:1D�5      # �  5      5      5      5      .��5!      � 	 � RowDefinitions	 5"   
   �� 5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      �� 5/      �� 50      �� 51      �� 52      �� 53      �� 54      
 � ColumnDefinitions
 56   
   �� 57      �� 58      �� 59      �� 5:      �� 5;      �� 5<      �� 5=      �� 5>      �� 5?      �� 5@      �� 5A      �� 5B      �� 5C      �� 5D      �� 5E      �� 5F      �� 5G      �� 5H      �� 5I      �� 5J      �� 5K      �� 5L      �� 5M      �� 5N      �� 5O      �� 5P      �� 5Q      �� 5R      .+�5V   
   �� $��21��6   $��6��6%   $��1��69   $��0��6J     TextBlockMainStyle#���  5W      $��Center=�5X      $��Center=�61     H1#���  5Y      
Overview6?   �� 5\   
   $��0��6   $��9��6!   $��2��65   $��13��6B    �� CornerRadius$ 3��6T     ThirdColorBrush#��[ 6e    J� Effect 5]      3  -System.Windows.Media.Effects.DropShadowEffect  5^         
BlurRadius$ 20Q�6#      ShadowDepth$	 0.9Q�5_   #     
FirstColor   Color# [ 5`   #   5b      �� 5d   
   $��1��6   $��9��6"   $��9��66   .+�5e      �� #���  6   $��0,0,0,5q�5f      Competitor list6:   �� 5g      $��0,4,0,0q�6   $	��330��6'   $	��250��64    �� AcceptsReturn . $��16(�5i        MainFont#���  5j      
������ 
�������$��0q�5m      
 � Tag$ instagram
ilonmask
shrek��5n       �� TextWrapping$
 Wrap=�5o      ��5p      �� *CollectFrom+ �� UpdateSourceTrigger$ PropertyChanged=�.��6W   5q      5r      �� 5t   
   $��2��6   $��22��6"   $��4��63   $��15��6G   $��Center=�6Z    �� Orientation$ Vertical=�6u   .+�5u      ��   MainButtonStyle#���  6   $��40��5v      ��5w      �� *StartAudienceCommand+��5x      �� *ButtonContent+�� 5{      -   5         ��Name$ Status��5|      $��Center=�5{      
�������$��0,20,0,5q�5~       
	 Bigger#��� 	 5       
 TextBlockThinStyle#��� 
 5�      ��5�      �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5�      /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  5�         Value$ WORKING��6$   :  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger   Binding 64   �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5�      <  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction   
Storyboard# [ 64      ControlStoryboardOption$
 Play=�6]   5�        5�      $	 OFF��6$    60   �� *Stats.Status+. 5�        # [ 64   $
 Stop=�6]   5�      5�      �� 5�      #���  6   $��Center=�5�      $��
ExtraLight"�5�      $��0,10,0,0q�5�      $��Technical account��5�      �� 5�      $��Center=�6   
�������$��15(�5�      $��0,0,0,5q�5�      $��
ExtraLight"�5�      ��60   �� *Stats.TechAccount+�� 5�      #���  6   $��Center=�5�      $��
ExtraLight"�5�      $��0,10,0,0q�5�      $��
Competitor��5�      �� 5�      $��Center=�6   
�������$��13(�5�      $��0,0,0,5q�5�      $��
ExtraLight"�5�      ��60   �� *Stats.Competitor+�� 5�      #���  6   $��Center=�5�      $��
ExtraLight"�5�      $��0,10,0,0q�5�      $��Audience count��5�      �� 5�      $��Center=�6   
�������$��15(�5�      $��0,0,0,5q�5�      $��
ExtraLight"�5�      ��60   �� *Stats.Count+�� 5�      $��Target��6   #���  6&   �� 5�        ComboBoxStyle#���  6    �� SelectedIndex 5�      �� *ComboBoxSelectedIndex+{�5�      �� *ComboBoxEnable+.@�5�      �� $��	Followers��6   �� 5�      $��	Following��6   5�      �� 5�      $��Center=�6   $��0,50,0,0q�66   .+�5�      �� $��Center=�6    $��0,0,0,5q�5�      
�������#��� 	 5�        TextBlockExtraThinStyle#���  5�      ��5�      �� *Stats.TimeInWork+�� 5�      $��Center=�6    #��� 
 5�      $��Time in work��5�      5�      5�      �� 5�   
   $ Vertical=�6   $��16��6,   $��2��6:   $��0��5�      $��20��6%   .+�5�      �� #��� 
 6   $��0,5,0,0q�5�      ��5�      �� *AudienceProcess.Message+5�      �� 5�      $ 
Horizontal=�6   $��Center=�62   .+�5�      �  � Maximum$	 100Q�6    � Minimum$ 0Q�5�      $��10��5�      $	��530��5�        SecondColorBrush#���  5�       � Value 5�      �� *AudienceProcess.Percent+�� 5�      $��35��6   $��5,0,0,3q�6'   #���  68   ��5�      �� *AudienceProcess.Percent+  �� StringFormat$  {0:0}%��5�      5�      5�      5�      