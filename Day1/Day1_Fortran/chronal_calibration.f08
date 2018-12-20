module day1
   implicit none

contains
   subroutine require_min_size(array, min_size)
      integer, allocatable, dimension(:)  :: array, array_copy
      integer                             :: min_size, old_size, new_size, err

      if (.not. allocated(array)) stop "require_min_size cannot be called with unallocated array"
      old_size = size(array)
      if (old_size >= min_size) return

      allocate (array_copy, source=array, stat=err)
      if (err /= 0) stop "array_copy: Allocation request denied"

      deallocate (array, stat=err)
      if (err /= 0) stop "array: Deallocation request denied"

      new_size = old_size + 100
      allocate (array(new_size), source=array_copy, stat=err)
      if (err /= 0) stop "array: Allocation request denied"

      if (allocated(array_copy)) deallocate (array_copy, stat=err)
      if (err /= 0) stop "array_copy: Deallocation request denied"
   end subroutine require_min_size

   logical function arr_contains(array, value)
      integer, allocatable, dimension(:)  :: array
      integer                             :: value, i

      ! yes, writing it yourself makes it faster. this uses 1/3 the cpu time the intrinsic function needs
      arr_contains = .false.
      do i = 1, size(array)
         if (array(i) .eq. value) then
            arr_contains = .true.
            return
         end if
      end do
   end function arr_contains

end module day1

program chronal_calibration
   use Day1
   implicit none

   integer                            :: freq_change, current_freq, double_freq
   integer                            :: ios, err, seen_freqs_index
   integer, allocatable, dimension(:) :: seen_freqs
   logical                            :: double_freq_found

   integer, parameter                 :: input_unit = 142

   ! Open the input file
   open (unit=input_unit, file="../input.txt", iostat=ios, status="old", action="read", form="formatted")
   if (ios /= 0) stop "Error opening input file"

   ! initial seen_freqs array size
   allocate (seen_freqs(100), stat=err)
   if (err /= 0) stop "seen_freqs: Allocation request denied"

   current_freq = 0
   double_freq = 0
   seen_freqs_index = 1
   double_freq_found = .false.
   do
      ! Read a line from opened file into line
      read (input_unit, *, iostat=ios) freq_change
      if (ios /= 0) exit

      ! change current frequency according to change read from file
      current_freq = current_freq + freq_change
      write (*, "(i5)", advance='NO') current_freq

      ! keep track of past frequencies to find duplicate (if not already found)
      if (.not. double_freq_found) then
         if (arr_contains(seen_freqs, current_freq)) then ! already in list -> duplicate found
            double_freq = current_freq
            double_freq_found = .true.
            print *, " - is a duplicate "
         else ! not in list -> add to list
            ! make sure array is big enough, resize if necessary
            call require_min_size(seen_freqs, seen_freqs_index)

            seen_freqs(seen_freqs_index) = current_freq
            seen_freqs_index = seen_freqs_index + 1

            print *, " - Added value to list"
            if (.not. arr_contains(seen_freqs, current_freq)) stop "message"
         end if
      end if
   end do

   if (ios == -1) then ! EOF
      print *, "End of file reached."
      print *, "Resulting frequency: ", current_freq
      print *, "First double frequency: ", double_freq
   else ! some other IO error
      print *, "The IO status is not 0, so the end of the file is probably reached."
      print *, "IO status code: ", ios
   end if

   ! deallocate array of seen frequencies
   if (allocated(seen_freqs)) deallocate (seen_freqs, stat=err)
   if (err /= 0) stop "seen_freqs: Deallocation request denied"
end program chronal_calibration
